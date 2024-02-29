using log4net;
using log4net.Config;
using Newtonsoft.Json.Linq;
using TicTacToe.BusinessLogic;
using TicTacToe.Entity;
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
builder.Configuration.AddJsonFile("connectionStrings.json",
        optional: false,
        reloadOnChange: true);

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
app.UseSwagger();
app.UseSwaggerUI();
//}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
