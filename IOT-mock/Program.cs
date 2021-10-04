using System;
using IOT_mock.Connector;
using IOT_mock.Connector.Models;
using IOT_mock.Connector.secret;
using IOT_mock.IotDevices;
using IOT_mock.IotDevices.Impl;
using IOT_mock.Sensors.Impl;
using IOT_mock.Sensors.Models;

namespace IOT_mock
{
    public class ProgramRun
    {
        public static void Main(string[] args)
        {
            var deviceId = Guid.Parse("c6ac2002-3203-408c-b87b-b77a1de2f6ac");
            IIotDevice device = new Pie
            {
                Id = deviceId,
                CommunicationClient = new MqttCommunicationClient
                {
                    Config = new ConnectorConfig
                    {
                        Prefix = "smart-cup",
                        Password = SecretConfig.Password,
                        Username = SecretConfig.Username,
                        ClientId = $"SmartCup-{deviceId}",
                        HostName = SecretConfig.Hostname,
                        IotId = deviceId,
                        Port = SecretConfig.Port
                    }
                }
            };
            device.AddSenor(new TempSensor
            {
                Configuration = new SensorConfiguration
                {
                    Id = Guid.Parse("b6cd3113-4314-519d-c98c-c88b2ef3f7bd"),
                    RecordInterval = 5000,
                    Suffix = "temp"
                }
            });

            device.StartDevice();
            Console.WriteLine("Device: ON");
            Console.ReadKey(true);
            device.StopDevice();
            Console.WriteLine("Device: OFF");
        }
    }
}