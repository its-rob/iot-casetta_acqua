using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json; // Assicurati di aggiungere il pacchetto NuGet Newtonsoft.Json
using NetCoreClient.Sensors;

public class Program
{
    public static async Task Main(string[] args)
    {
        // URL del server locale (assicurati che sia corretto)
        string url = "https://0bd5-151-34-52-167.ngrok-free.app/";

        var water_usage = new VirtualWaterUsageSensor();
        var temperatureValue = new VirtualWaterTempSensor();
        var filter_wear = new VirtualFilterWearSensor();
        var status = new VirtualWaterStatusSensor();



        // Dati da inviare
        //int temperatureValue = 25; // Temperatura in intero
        //string status = "OK"; // Status del dispositivo
        //int filter_wear = 10; // Usura filtro (in percentuale)
        //int waterValue = 200; // Litri erogati

        // Creazione del JSON con la struttura specificata
        var payload = new
        {
            temperature = temperatureValue.WaterTemperature(),
            water_dispensed = water_usage.WaterUsage(),
            filter_wear = filter_wear.FilterWear(),
            status = status.WaterStatus(),
        };

        // Serializza l'oggetto in JSON
        string json = JsonConvert.SerializeObject(payload);

        // Configura HttpClient
        using var client = new HttpClient();
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        try
        {
            // Effettua la richiesta POST
            HttpResponseMessage response = await client.PostAsync(url, content);

            // Controlla se la richiesta ha avuto successo
            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("Dati inviati correttamente!");
            }
            else
            {
                Console.WriteLine($"Errore: {response.StatusCode}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Errore durante la richiesta: {ex.Message}");
        }
    }
}