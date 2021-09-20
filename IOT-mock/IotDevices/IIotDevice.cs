using System;
using System.Collections.Generic;
using IOT_mock.Connector;
using IOT_mock.Sensors;
using IOT_mock.Sensors.Models;

namespace IOT_mock.IotDevices.Impl
{
    public interface IIotDevice
    {
        IList<ISensor> Sensors { get; init; }
        IConnector Connector { get; init; }
        void AddSenor(params ISensor[] sensors);
        IEnumerable<SensorConfiguration> GetSensorConfigurations();
        SensorConfiguration GetSensorConfiguration(Guid id);
        void StartAllSenors();
        void StartSenors(Guid id);
        void StopAllSensors();
        void StopSensors(Guid id);

        void StartDevice();
        
        void StopDevice();
    }
}