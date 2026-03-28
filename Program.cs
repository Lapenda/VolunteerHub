using DotNetEnv;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using VolunteerHub.Database;
using VolunteerHub.Services;
using VolunteerHub.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

Env.Load();

string connectionKey = builder.Environment.IsDevelopment()
    ? "DEBUG_CONNECTION"
    : "PROD_CONNECTION";

var connectionString = Environment.GetEnvironmentVariable(connectionKey);

if (string.IsNullOrEmpty(connectionString))
{
    throw new Exception($"Connection string for {connectionKey} was not found in .env file!");
}

Console.WriteLine($"**** CURRENT ENVIRONMENT: {builder.Environment.EnvironmentName} ****");

// Add services to the container.
builder.Services.AddDbContext<DBM>(options =>
    options.UseSqlServer(connectionString)
);

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddOptions();

builder.Services.AddHttpContextAccessor();

builder.Services.AddScoped<IAccountService, AccountService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
