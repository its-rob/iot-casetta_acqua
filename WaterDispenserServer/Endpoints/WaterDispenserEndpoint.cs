namespace WaterDispenserServer.Endpoints;

using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using WaterDispenserServer.Models;
using WaterDispenserServer.Services;

public static class WaterDispenserEndpoints
{
    public static IEndpointRouteBuilder MapCustomersEndpoints(
                                            this IEndpointRouteBuilder builder)
    {
        var wd_sensorsGroup = builder.MapGroup("/")
                                     .WithOpenApi()
                                     .WithTags("WD_Sensors");

        wd_sensorsGroup.MapPost("/", InsertSensorValuesAsync)
                     .WithName("InsertSensorValue");

        return builder;
    }

    private static async Task<NoContent> InsertSensorValuesAsync([FromBody] WaterTemperature wt, WaterDispenser data)
    {
        await data.InsertSensorValueAsync(wt);
        return TypedResults.NoContent();
    }
}