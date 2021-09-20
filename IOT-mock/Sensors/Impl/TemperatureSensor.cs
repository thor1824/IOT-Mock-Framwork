using System;
using System.Threading;
using System.Threading.Tasks;
using ConsolePublisher;
using MQTTnet;

namespace IOT_mock
{
    public interface ISensor
    {
        SensorConfiguration Configuration { get; set; }
        Task StartRecordingAsync(OnSensorDataRecorded<SenorResponse> action);
        void StopRecording();
    }

    public class TempSensor : ISensor
    {
        public SensorConfiguration Configuration { get; set; }
        private bool isOn = false;
        private string type = "temperature";
        private double startValue = 20.0;
        private int incrementBuffer = 1;
        
        
        public async Task StartRecordingAsync(OnSensorDataRecorded<SenorResponse> action)
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
                    Value = "" + value
                });
                
                Thread.Sleep(Configuration.RecordInterval);
            }
        }
        
        public void StopRecording()
        {
            isOn = false;
        }
        
        
        
        private static void SimulatePublish(string topicSuffix, double startValue, int incrementMax, CancellationToken token)
        {
            Console.WriteLine(topicSuffix + " Simulation Start");
            var oldValue = startValue;
            
            while (/*!token.IsCancellationRequested*/ true)
            {
                Random rnd = new Random();
                var modifier = 0.2 *(rnd.Next(-1 * incrementMax, 0) * rnd.NextDouble() + rnd.Next(0, incrementMax + 1) * rnd.NextDouble());
                
                var value = oldValue + modifier;
                oldValue = value;
                
                var testMessage = new MqttApplicationMessageBuilder()
                    .WithTopic($"iot-{topicSuffix}")
                    .WithPayload($"{value}")
                    .WithExactlyOnceQoS()
                    .WithRetainFlag()
                    .Build();


                if (_client.IsConnected)
                {
                    Console.WriteLine($"publishing at {DateTime.UtcNow}");
                    _client.PublishAsync(testMessage);
                }
                Thread.Sleep(2000);
            }
        }
        
        
    }
    
    public class SenorResponse
    {
        public Guid SensorId { get; set; }
        public string Value { get; set; }
    }
    

    public class SensorConfiguration
    {
        
        public Guid Id { get; set; }
        public int RecordInterval  { get; set; }
    }
}