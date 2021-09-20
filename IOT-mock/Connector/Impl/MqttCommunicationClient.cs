using System.Security.Authentication;
using IOT_mock.Connector.Models;
using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Client.Options;

namespace IOT_mock.Connector
{
    public class MqttCommunicationClient : ICommunicationClient
    {
        private IMqttClient _client;
        private IMqttClientOptions _options;
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
                .WithClientId(Config.ClientId)
                .WithTcpServer(Config.HostName, Config.Port)
                .WithCredentials(Config.Username, Config.Password)
                .WithCleanSession()
                .Build();

            _client.ConnectAsync(_options).Wait();
            /*
             _client.UseConnectedHandler(e =>
                {
                    Console.WriteLine("Connected successfully with MQTT Brokers.");
                });
                _client.UseDisconnectedHandler(e =>
                {
                    Console.WriteLine("Disconnected from MQTT Brokers.");
                });
                
                
                _client.UseApplicationMessageReceivedHandler(e =>
                {
                    try
                    {
                        string topic = e.ApplicationMessage.Topic;
                        if (string.IsNullOrWhiteSpace(topic) == false)
                        {
                            string payload = Encoding.UTF8.GetString(e.ApplicationMessage.Payload);
                            Console.WriteLine($"Topic: {topic}. Message Received: {payload}");
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message, ex);
                    }
                });
             */
        }

        public void SendData(string topicSuffix, string payload)
        {
            var testMessage = new MqttApplicationMessageBuilder()
                .WithTopic($"{Config.Prefix}/{topicSuffix}")
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