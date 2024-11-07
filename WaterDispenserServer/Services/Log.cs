namespace WaterDispenserServer.Services;
using Dapper;
using WaterDispenserServer.Models;
using MySql.Data.MySqlClient;

public class Log
{
    private readonly string _connectionString;

    public Log(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("db");
    }

    public async Task InsertTemperatureValueAsync(LogModel log)
    {
        using var connection = new MySqlConnection(_connectionString);
        await connection.OpenAsync();

        const string query = """
            INSERT INTO log (temperature, water_dispensed, filter_wear, status, date)
            VALUES (@Temperature, @Water_Dispensed, @Filter_Wear, @Status, @Date)
            """;

        await connection.ExecuteAsync(query, log);
        Console.WriteLine("\nData Written Correctly!\n");
    }
}