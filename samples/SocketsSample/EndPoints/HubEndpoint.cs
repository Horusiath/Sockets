using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Channels;
using Microsoft.AspNetCore.Sockets;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using SocketsSample.Hubs;

namespace SocketsSample
{
    public class HubEndpoint : RpcEndpoint
    {
        private readonly ILogger<HubEndpoint> _logger;
        private readonly Bus _bus = new Bus();

        public HubEndpoint(ILogger<HubEndpoint> logger, ILogger<RpcEndpoint> rpcLogger, IRpcMethodProvider provider)
            : base(rpcLogger, provider)
        {
            _logger = logger;
        }

        public override Task OnConnected(Connection connection)
        {
            // TODO: Get the list of hubs and signals over the connection

            // Subscribe to the hub
            _bus.Subscribe(nameof(Chat), message => OnMessage(connection, message));

            // Subscribe to the connection id
            _bus.Subscribe(connection.ConnectionId, message => OnMessage(connection, message));

            return base.OnConnected(connection);
        }

        private Task OnMessage(Connection connection, Message message)
        {
            return connection.Channel.Output.WriteAsync(message.Payload);
        }

        public Task Invoke(string key, string method, object[] args)
        {
            //var obj = new JObject();
            //obj["method"] = method;
            //obj["params"] = new JArray(args.Select(a => JToken.FromObject(a)).ToArray());

            //if (_logger.IsEnabled(LogLevel.Debug))
            //{
            //    _logger.LogDebug("Outgoing RPC invocation method '{methodName}' to {signal}", method, key);
            //}

            //return _bus.Publish(key, new Message
            //{
            //    Payload = Encoding.UTF8.GetBytes(obj.ToString())
            //});
            return Task.CompletedTask;
        }

        //protected override void Initialize(Connection connection, object endpoint)
        //{
        //    //var hub = (Hub)endpoint;
        //    //hub.Clients = new HubConnectionContext(endpoint.GetType().Name, this);
        //    //hub.ConnectionId = connection.ConnectionId;
        //    //hub.ConnectionMetadata = connection.Metadata;
        //}
    }
}
