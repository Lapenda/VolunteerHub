using DotNetEnv;
using Microsoft.EntityFrameworkCore;
using Serilog;
using VolunteerHub.Database;
using VolunteerHub.Filters;
using VolunteerHub.Installers;
using VolunteerHub.Middleware;

var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .MinimumLevel.Override("Microsoft", Serilog.Events.LogEventLevel.Information)
    .WriteTo.Console(restrictedToMinimumLevel: Serilog.Events.LogEventLevel.Debug)
    .WriteTo.File("logs/error-log.txt", rollingInterval: RollingInterval.Day, restrictedToMinimumLevel: Serilog.Events.LogEventLevel.Warning)
    .CreateLogger();

builder.Host.UseSerilog();

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

builder.Services.AddSecurity(builder.Configuration);

builder.Services.AddControllers(options =>
{
    options.Filters.Add<ServiceResultFilter>();
});

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddOptions();

builder.Services.AddHttpContextAccessor();

builder.Services.AddServices(builder.Configuration);

var app = builder.Build();

app.UseMiddleware<ExceptionMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
