using System.Text.Json;

namespace NetCoreClient.Sensors
{
    class VirtualFilterWearSensor : ISensorInterface
    {
        private readonly Random Random;
        private int wearPercentage;

        public VirtualFilterWearSensor()
        {
            Random = new Random();
            wearPercentage = Random.Next(1, 20);
        }

        public int FilterWear()
        {
            wearPercentage = Math.Min(100, wearPercentage + Random.Next(1, 3));
            return wearPercentage;
        }

        public string ToJson()
        {
            return JsonSerializer.Serialize(FilterWear());
        }
    }
}
