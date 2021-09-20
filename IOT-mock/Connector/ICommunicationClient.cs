using System.Security.Authentication;
using IOT_mock.Connector.Models;
using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Client.Options;

namespace IOT_mock.Connector
{
    public interface IConnector
    {
        void Connect();
        void SendData(string topicSuffix, string payload);
        void Disconnect();
        ConnectorConfig Config { get; init; }
    }

    public class MqttConnector : IConnector
    {
        private readonly IMqttClient _client;
        private readonly IMqttClientOptions _options;
        public ConnectorConfig Config { get; init; }

        public void Connect()
        {
            
            
            var factory = new MqttFactory();
            _client = factory.CreateMqttClient();

            //configure options
            _options = new MqttClientOptionsBuilder()
                .WithTls(new MqttClientOptionsBuilderTlsParameters
                {
                    UseTls = true,
                    SslProtocol = SslProtocols.Tls12
                })
                .WithClientId(_config.ClientId)
                .WithTcpServer(_config.HostName, _config.Port)
                .WithCredentials(_config.Username, _config.Password)
                .WithCleanSession()
                .Build();

            _client.ConnectAsync(_options).Wait();
        }

        public void SendData(string topicSuffix, string payload)
        {
            var testMessage = new MqttApplicationMessageBuilder()
                .WithTopic($"{_config.Prefix}-{topicSuffix}")
                .WithPayload($"{payload}")
                .WithExactlyOnceQoS()
                .WithRetainFlag()
                .Build();


            if (_client.IsConnected)
            {
                _client.PublishAsync(testMessage);
            }
        }

        public void Disconnect()
        {
            _client.DisconnectAsync().Wait();
        }
    }
}