using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using IOT_mock.Connector;
using IOT_mock.IotDevices.Models;
using IOT_mock.Sensors;
using IOT_mock.Sensors.Models;

namespace IOT_mock.IotDevices.Impl
{
    public class Pie : IIotDevice
    {
        public Guid Id { get; init; }
        public IList<ISensor> Sensors { get; init; } = new List<ISensor>();
        public ICommunicationClient CommunicationClient { get; init; }

        public void AddSenor(params ISensor[] sensors)
        {
            foreach (var sensor in sensors)
            {
                Sensors.Add(sensor);    
            }
        }

        public IEnumerable<SensorConfiguration> GetSensorConfigurations()
        {
            return Sensors.Select(sensor => sensor.Configuration);
        }
        
        public SensorConfiguration GetSensorConfiguration(Guid id)
        {
            return Sensors.FirstOrDefault(sensor => sensor.Configuration.Id == id)?.Configuration;
        }

        public void StartAllSenors()
        {
            foreach (var sensor in Sensors)
            {
                Task.Run(() =>
                {
                    Console.WriteLine($"Sensor {sensor.Configuration.Id}, Is On");
                    sensor.StartRecordingAsync(response =>
                    {
                        var clientResponse = new ClientResponse
                        {
                            Id = Id,
                            Body = response
                        };
                        var json = JsonSerializer.Serialize(clientResponse);
                        CommunicationClient.SendData(sensor.Configuration.Suffix, json);
                    });
                });
            }
        }

        public void StartSenors(Guid id)
        {
           var sensor = Sensors.FirstOrDefault(sensor => sensor.Configuration.Id == id);

           sensor?.StartRecordingAsync(response =>
           {
               CommunicationClient.SendData("Test", response.Value);
           });
        }

        public void StopAllSensors()
        {
            foreach (var sensor in Sensors)
            {
                sensor.StopRecording();
            }
        }

        public void StopSensors(Guid id)
        {
            var sensor = Sensors.FirstOrDefault(sensor => sensor.Configuration.Id == id);

            sensor?.StopRecording();
        }

        public void StartDevice()
        {
            CommunicationClient.Connect();
            StartAllSenors();
        }

        public void StopDevice()
        {
            StopAllSensors();
            CommunicationClient.Disconnect();
        }
    }
}