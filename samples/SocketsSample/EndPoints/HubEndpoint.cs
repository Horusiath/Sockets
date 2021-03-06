﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Channels;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using SocketsSample.Hubs;

namespace SocketsSample
{
    public class HubEndpoint : JsonRpcEndpoint, IHubConnectionContext
    {
        private readonly ILogger<HubEndpoint> _logger;

        public HubEndpoint(ILogger<HubEndpoint> logger, ILogger<JsonRpcEndpoint> jsonRpcLogger, IServiceProvider serviceProvider)
            : base(jsonRpcLogger, serviceProvider)
        {
            _logger = logger;
            All = new AllClientProxy(this);
        }

        public IClientProxy All { get; }

        public IClientProxy Client(string connectionId)
        {
            return new SingleClientProxy(this, connectionId);
        }

        private byte[] Pack(string method, object[] args)
        {
            var obj = new JObject();
            obj["method"] = method;
            obj["params"] = new JArray(args.Select(a => JToken.FromObject(a)).ToArray());

            if (_logger.IsEnabled(LogLevel.Debug))
            {
                _logger.LogDebug("Outgoing RPC invocation method '{methodName}'", method);
            }

            return Encoding.UTF8.GetBytes(obj.ToString());
        }

        protected override void Initialize(object endpoint)
        {
            ((Hub)endpoint).Clients = this;
            base.Initialize(endpoint);
        }

        protected override void DiscoverEndpoints()
        {
            // Register the chat hub
            RegisterJsonRPCEndPoint(typeof(Chat));
        }

        private class AllClientProxy : IClientProxy
        {
            private readonly HubEndpoint _endPoint;

            public AllClientProxy(HubEndpoint endPoint)
            {
                _endPoint = endPoint;
            }

            public Task Invoke(string method, params object[] args)
            {
                // REVIEW: Thread safety
                var tasks = new List<Task>(_endPoint.Connections.Count);

                byte[] message = null;

                foreach (var connection in _endPoint.Connections)
                {
                    if (message == null)
                    {
                        message = _endPoint.Pack(method, args);
                    }

                    tasks.Add(connection.Channel.Output.WriteAsync(message));
                }

                return Task.WhenAll(tasks);
            }
        }

        private class SingleClientProxy : IClientProxy
        {
            private readonly string _connectionId;
            private readonly HubEndpoint _endPoint;

            public SingleClientProxy(HubEndpoint endPoint, string connectionId)
            {
                _endPoint = endPoint;
                _connectionId = connectionId;
            }

            public Task Invoke(string method, params object[] args)
            {
                var connection = _endPoint.Connections[_connectionId];
                return connection?.Channel.Output.WriteAsync(_endPoint.Pack(method, args));
            }
        }
    }
}
