using TicTacToe.ML;

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

TicTacToe.BusinessLogic.ComputerPlayerConfig.RegisterComputerPlayer(new TicTacToe.BusinessLogic.ComputerPlayerV2());

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
