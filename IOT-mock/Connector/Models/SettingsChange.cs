using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IOT_mock.Connector.Models
{
    public class SettingsChange
    {
        public string IotId { get; set; }
        public List<SensorSetting> SensorSettings { get; set; }
    }

    public class SensorSetting
    {
        public string SensorId { get; set; }
        public int Interval { get; set; }
    }
}
