using System;
using System.Collections.Generic;
using IOT_mock.Connector;
using IOT_mock.Connector.Models;
using IOT_mock.Sensors;
using IOT_mock.Sensors.Models;

namespace IOT_mock.IotDevices
{
    public interface IIotDevice
    {
        IList<ISensor> Sensors { get; init; }
        ICommunicationClient CommunicationClient { get; init; }
        Guid Id { get; init; }
        void AddSenor(params ISensor[] sensors);
        IEnumerable<SensorConfiguration> GetSensorConfigurations();
        SensorConfiguration GetSensorConfiguration(Guid id);
        void StartAllSenors();
        void StartSenors(Guid id);
        void StopAllSensors();
        void StopSensors(Guid id);
        void ChangeSettingsForSensor(Guid id, SettingsChange settings);
        void StartDevice();
        
        void StopDevice();
    }
}