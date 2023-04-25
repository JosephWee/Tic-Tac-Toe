using Newtonsoft.Json.Linq;
using TicTacToe.BusinessLogic;
using TicTacToe.Entity;
using TicTacToe.ML;
using static TicTacToe.BusinessLogic.ComputerPlayerV3;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add connection strings
builder.Configuration.AddJsonFile("connectionStrings.json",
        optional: false,
        reloadOnChange: true);

var app = builder.Build();

string msbuildDir = new DirectoryInfo(Path.Combine(AppContext.BaseDirectory, @"..\..\..")).FullName;
string MLModel1Path = Path.Combine(msbuildDir, "MLModels", "MLModel1.zip");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
