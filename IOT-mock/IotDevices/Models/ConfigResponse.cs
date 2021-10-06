using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IOT_mock.IotDevices.Models
{
    public class ConfigResponse
    {
        public Guid IotId { get; set; }
        public List<SensorSettingResponse> SensorConfigs { get; set; }

        public ConfigResponse(Guid Id)
        {
            IotId = Id;
            SensorConfigs = new List<SensorSettingResponse>();
        }
    }

    public class SensorSettingResponse
    {
        public Guid SensorId { get; set; }
        public int Interval { get; set; }
    }
}
