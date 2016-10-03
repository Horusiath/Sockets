using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Channels;
using Microsoft.AspNetCore.Sockets;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace SocketsSample
{
    public class RpcInvocation
    {
        public string Id { get; set; }
        public string Method { get; set; }
        public object[] Args { get; set; }
    }

    public class RpcResult
    {
        public string Id { get; set; }
        public string Error { get; set; }
        public object Result { get; set; }
    }

    public interface IRpcMethodProvider
    {
        IRpcMethod GetMethod(string methodName);
    }

    public interface IRpcMethod
    {
        string Method { get; }
        Type[] Args { get; }
        object Invoke(object[] args);
    }

    public interface IRpcFormatter
    {
        RpcInvocation ReadInvocation(Stream stream);
        object[] BindArguments(IRpcMethod method, RpcInvocation invocation);
        void WriteInvocation(RpcInvocation invocation, Stream stream);
        void WriteResult(RpcResult response, Stream stream);
    }

    public class JsonRpcFormatter : IRpcFormatter
    {
        public object[] BindArguments(IRpcMethod method, RpcInvocation invocation)
        {
            var @params = (JArray)invocation.Args[0];
            var args = @params.Zip(method.Args, (a, p) => a.ToObject(p)).ToArray();
            return args;
        }

        public RpcInvocation ReadInvocation(Stream stream)
        {
            var reader = new JsonTextReader(new StreamReader(stream));
            reader.SupportMultipleContent = true;

            while (true)
            {
                while (!reader.Read())
                {
                    break;
                }

                JObject request = JObject.Load(reader);

                return new RpcInvocation
                {
                    Id = request.Value<string>("id"),
                    Method = request.Value<string>("method"),
                    Args = new[] { request.Value<JArray>("params") }
                };
            }
        }

        public void WriteInvocation(RpcInvocation invocation, Stream stream)
        {
            var obj = new JObject();
            obj["method"] = invocation.Method;
            obj["params"] = new JArray(invocation.Args.Select(a => JToken.FromObject(a)).ToArray());

            var writer = new JsonTextWriter(new StreamWriter(stream));
            obj.WriteTo(writer);
            writer.Flush();
        }

        public void WriteResult(RpcResult response, Stream stream)
        {
            var obj = new JObject();
            obj["result"] = JValue.FromObject(response.Result);
            obj["id"] = response.Id;
            obj["error"] = response.Error;
            var writer = new JsonTextWriter(new StreamWriter(stream));
            obj.WriteTo(writer);
            writer.Flush();
        }
    }

    public class RpcEndpoint : EndPoint
    {
        private readonly IRpcMethodProvider _methodProvider;
        private readonly ILogger<RpcEndpoint> _logger;

        public RpcEndpoint(ILogger<RpcEndpoint> logger, IRpcMethodProvider methodProvider)
        {
            _logger = logger;
            _methodProvider = methodProvider;
        }

        public override async Task OnConnected(Connection connection)
        {
            IRpcFormatter formatter = GetFormatter(connection);

            connection.Metadata["rpc-formatter"] = formatter;

            // TODO: Dispatch from the caller
            await Task.Yield();

            // DO real async reads
            var stream = connection.Channel.GetStream();

            while (true)
            {

                RpcInvocation invocation = null;
                try
                {
                    invocation = formatter.ReadInvocation(stream);
                }
                catch (Exception)
                {
                    if (connection.Channel.Input.Reading.IsCompleted)
                    {
                        break;
                    }

                    throw;
                }

                IRpcMethod method = _methodProvider.GetMethod(invocation.Method);
                if (method != null)
                {
                    invocation.Args = formatter.BindArguments(method, invocation);

                    var result = new RpcResult();
                    result.Id = invocation.Id;
                    try
                    {
                        var methodResult = method.Invoke(invocation.Args);
                        result.Result = methodResult;
                    }
                    catch (Exception ex)
                    {
                        result.Error = ex.Message;
                    }

                    formatter.WriteResult(result, stream);
                }
            }
        }

        private IRpcFormatter GetFormatter(Connection connection)
        {
            return new JsonRpcFormatter();
        }
    }

    public class ReflectedRpcMethodProvider : IRpcMethodProvider
    {
        private readonly Dictionary<string, IRpcMethod> _callbacks = new Dictionary<string, IRpcMethod>(StringComparer.OrdinalIgnoreCase);
        private readonly ILogger _logger;

        public ReflectedRpcMethodProvider(ILogger logger)
        {
            _logger = logger;
        }

        public IRpcMethod GetMethod(string methodName)
        {
            IRpcMethod method;
            if (_callbacks.TryGetValue(methodName, out method))
            {
                return method;
            }
            return null;
        }

        public void Add(Type type)
        {
            var methods = new List<string>();

            foreach (var m in type.GetTypeInfo().DeclaredMethods.Where(m => m.IsPublic))
            {
                var methodName = type.FullName + "." + m.Name;

                methods.Add(methodName);

                var parameters = m.GetParameters();

                if (_callbacks.ContainsKey(methodName))
                {
                    throw new NotSupportedException(String.Format("Duplicate definitions of {0}. Overloading is not supported.", m.Name));
                }

                if (_logger.IsEnabled(LogLevel.Debug))
                {
                    _logger.LogDebug("RPC method '{methodName}' is bound", methodName);
                }

                _callbacks[methodName] = new RpcMethod(m);
            };
        }

        private class RpcMethod : IRpcMethod
        {
            private MethodInfo _methodInfo;

            public RpcMethod(MethodInfo methodInfo)
            {
                _methodInfo = methodInfo;
            }

            public Type[] Args => _methodInfo.GetParameters().Select(p => p.ParameterType).ToArray();

            public string Method => _methodInfo.Name;

            public object Invoke(object[] args)
            {
                object value = Activator.CreateInstance(_methodInfo.DeclaringType);

                return _methodInfo.Invoke(value, args);
            }
        }
    }
}
