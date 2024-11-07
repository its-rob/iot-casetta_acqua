using Microsoft.AspNetCore.Http.HttpResults;
using WaterDispenserServer.Endpoints;
using WaterDispenserServer.Models;
using WaterDispenserServer.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<WaterDispenser>();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

//app.MapGet("/", () => "Hello World!");

app.MapCustomersEndpoints();

app.Run();