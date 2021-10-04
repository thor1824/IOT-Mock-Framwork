using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IOT_mock.Connector.Models
{
    class SettingsTopicMessage
    {
        public string Pattern { get; set; }
        public SettingsChange Data { get; set; }
    }
}
