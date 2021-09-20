using System;

namespace IOT_mock.Sensors.Models
{
    public class SensorConfiguration
    {
        
        public Guid Id { get; set; }
        public int RecordInterval  { get; set; }
        public string Suffix  { get; set; }
    }
}