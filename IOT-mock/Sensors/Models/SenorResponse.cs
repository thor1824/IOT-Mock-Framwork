using System;

namespace IOT_mock.Sensors.Models
{
    public class SenorResponse
    {
        public Guid SensorId { get; set; }
        public string Value { get; set; }
        public string MeasurementUnit { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}