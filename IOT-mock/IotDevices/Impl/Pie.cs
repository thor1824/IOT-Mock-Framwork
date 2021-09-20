using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using ConsolePublisher;

namespace IOT_mock
{
    public class Pie
    {
        private readonly IList<ISensor> _sensors;
        
        public void AddSenor(params ISensor[] sensors)
        {
            foreach (var sensor in sensors)
            {
                _sensors.Add(sensor);    
            }
        }

        public IEnumerable<SensorConfiguration> GetSensorConfigurations()
        {
            return _sensors.Select(sensor => sensor.Configuration);
        }
        
        public SensorConfiguration GetSensorConfiguration(Guid id)
        {
            return _sensors.FirstOrDefault(sensor => sensor.Configuration.Id == id)?.Configuration;
        }

        public void StartAllSenors(OnSensorDataRecorded<SenorResponse> sensorFetch)
        {
            foreach (var sensor in _sensors)
            {
                sensor.StartRecordingAsync(sensorFetch.Invoke);
            }
        }

        public void StartSenors(Guid id, OnSensorDataRecorded<SenorResponse> sensorFetch)
        {
           var sensor = _sensors.FirstOrDefault(sensor => sensor.Configuration.Id == id);

           sensor?.StartRecordingAsync(sensorFetch.Invoke);
        }

        public void StopAllSensors()
        {
            foreach (var sensor in _sensors)
            {
                sensor.StopRecording();
            }
        }

        public void StopSensors(Guid id)
        {
            var sensor = _sensors.FirstOrDefault(sensor => sensor.Configuration.Id == id);

            sensor?.StopRecording();
        }
    }

    
}