using System;
using IOT_mock.Sensors.Models;

namespace IOT_mock.IotDevices.Models
{
    public class ClientResponse {
        public Guid Id { get; set; }
        public SenorResponse Body { get; set; }
    }
}