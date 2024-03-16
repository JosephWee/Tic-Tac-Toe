using log4net;
using log4net.Config;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json.Linq;
using TicTacToe.BusinessLogic;
using TicTacToe.Entity;
using TicTacToe.Cache;
using TicTacToe.ML;
using static TicTacToe.BusinessLogic.ComputerPlayerV3;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services
    .AddControllers()
    .AddJsonOptions(
        (jsonOptions) =>
            jsonOptions
            .JsonSerializerOptions
            .PropertyNamingPolicy = null);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

XmlConfigurator.ConfigureAndWatch(new FileInfo("log4net.config"));
builder.Services.AddSingleton(
    typeof(log4net.ILog),
    builder =>
    {
        return log4net.LogManager.GetLogger(typeof(Program));
    }
);
//builder.Services.AddSingleton(
//    log4net.LogManager.GetLogger(typeof(Program))
//);

// Add connection strings
//builder.Configuration.AddJsonFile("connectionStrings.json",
//        optional: false,
//        reloadOnChange: true);
builder.Configuration.AddEnvironmentVariables();

// Add services to the container.
var config = builder.Configuration;

builder.Services.AddDistributedSqlServerCache(options =>
{
    options.ConnectionString = config.GetValue<string>("TicTacToeWebApiCache") ?? string.Empty;
    options.SchemaName = "dbo";
    options.TableName = "TicTacToeWebApiCache";
});

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
if (app.Environment.EnvironmentName.StartsWith("Development"))
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

int? TicTacToeWebApiCacheExpiration = config.GetValue<int>("TicTacToeWebApiCacheExpiration");

app.Lifetime.ApplicationStarted.Register(() =>
{
    string currentTimeUTC = DateTime.UtcNow.ToString("O");

    var options =
        new DistributedCacheEntryOptions()
        .SetSlidingExpiration(TimeSpan.FromMinutes(TicTacToeWebApiCacheExpiration.HasValue ? TicTacToeWebApiCacheExpiration.Value : 60));
    var DistCache = app.Services.GetService<IDistributedCache>();
    if (DistCache == null)
        throw new Exception("TicTacToeWebApiCache registration failed.");

    DistCache.SetCache("AppStartTimeUTC", currentTimeUTC, options);
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
