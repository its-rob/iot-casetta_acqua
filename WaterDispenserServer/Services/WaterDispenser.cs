namespace WaterDispenserServer.Services;

using MySql.Data;
using Dapper;
using WaterDispenserServer.Models;
using MySql.Data.MySqlClient;

public class WaterDispenser
{
    private readonly string _connectionString;

    public WaterDispenser(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("db");
    }

    public async Task InsertSensorValueAsync(WaterTemperature wt)
    {
        using var connection = new MySqlConnection(_connectionString);
        await connection.OpenAsync();

        const string query = """
            INSERT INTO sensor_values (value)
            VALUES (@Temperature)
            """;

        await connection.ExecuteAsync(query, wt);
        Console.WriteLine("\nData Written Correctly!\n");
    }
}