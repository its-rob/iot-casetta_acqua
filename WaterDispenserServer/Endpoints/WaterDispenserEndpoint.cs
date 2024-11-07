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
        var logGroup = builder.MapGroup("/")
                                     .WithOpenApi()
                                     .WithTags("Log");

        logGroup.MapPost("/", InsertLogAsync)
                     .WithName("InsertLog");

        return builder;
    }

    private static async Task<NoContent> InsertLogAsync([FromBody] LogModel log, Log data)
    {
        await data.InsertTemperatureValueAsync(log);
        return TypedResults.NoContent();
    }
}