using System.Text.Json;

namespace NetCoreClient.Sensors
{
    class VirtualWaterUsageSensor : ISensorInterface
    {
        private readonly Random Random;
        private int totalLiters;

        public VirtualWaterUsageSensor()
        {
            Random = new Random();
            totalLiters = 0;
        }

        public int WaterUsage()
        {
            totalLiters += Random.Next(1, 5);
            return totalLiters;
        }

        public string ToJson()
        {
            return JsonSerializer.Serialize(WaterUsage());
        }
    }
}
