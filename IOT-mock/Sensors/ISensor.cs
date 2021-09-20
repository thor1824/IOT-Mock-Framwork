using System.Threading.Tasks;
using ConsolePublisher;

namespace IOT_mock
{
    public interface ISensor
    {
        SensorConfiguration Configuration { get; set; }
        Task StartRecordingAsync(OnSensorDataRecorded<SenorResponse> action);
        void StopRecording();
    }
}