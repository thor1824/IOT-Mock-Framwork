using System;
using System.Threading;
using System.Threading.Tasks;
using ConsolePublisher;
using IOT_mock.Sensors.Models;

namespace IOT_mock.Sensors.Impl
{
    public class TempSensor : ISensor
    {
        public SensorConfiguration Configuration { get; set; }
        private bool isOn = false;
        private string type = "temperature";
        private double startValue = 20.0;
        private int incrementBuffer = 1;
        
        
        public async Task StartRecordingAsync(OnSensorDataRecorded action)
        {
            isOn = true;
            var oldValue = startValue;
            
            while (isOn)
            {
                Random rnd = new Random();
                var modifier = 0.2 *(rnd.Next(-1 * incrementBuffer, 0) * rnd.NextDouble() + rnd.Next(0, incrementBuffer + 1) * rnd.NextDouble());
                
                var value = oldValue + modifier;
                oldValue = value;
                
                action.Invoke(new SenorResponse {
                    SensorId = Configuration.Id,
                    Value = "" + value,
                    MeasurementUnit = "temp/c",
                    TimeStamp = DateTime.UtcNow
                });
                Console.WriteLine(Configuration.RecordInterval);
                Thread.Sleep(Configuration.RecordInterval);
            }
        }
        
        public void StopRecording()
        {
            isOn = false;
        }
    }
}