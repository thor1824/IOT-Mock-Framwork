using IOT_mock.Connector.Models;

namespace IOT_mock.Connector
{
    public interface ICommunicationClient
    {
        void Connect();
        void SendData(string topicSuffix, string payload);
        void Disconnect();
        ConnectorConfig Config { get; init; }
        OnSettingsChange OnSettingsChange { get; set; }
    }
}