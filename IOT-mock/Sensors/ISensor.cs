using System.Threading.Tasks;
using ConsolePublisher;
using IOT_mock.Sensors.Models;

namespace IOT_mock.Sensors
{
    public interface ISensor
    {
        SensorConfiguration Configuration { get; set; }
        Task StartRecordingAsync(OnSensorDataRecorded action);
        void StopRecording();
    }
}