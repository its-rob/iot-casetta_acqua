using System.Text.Json;

namespace NetCoreClient.Sensors
{
    class VirtualWaterStatusSensor
    {
        private readonly string[] statusOptions = { "in funzione", "spenta", "in anomalia" };
        private readonly Random Random;

        public VirtualWaterStatusSensor()
        {
            Random = new Random();
        }

        public string WaterStatus()
        {
            return statusOptions[Random.Next(statusOptions.Length)];
        }
    }
}
