using System;

namespace IOT_mock.Connector.Models
{
    public class ConnectorConfig
    {
        public string Prefix { get; init; }
        public Guid IotId { get; init; }
        public string HostName { get; init; }
        public string ClientId { get; init; }
        public string Username { get; init; }
        public string Password { get; init; }
        public int Port { get; set; }
    }
}