using BlazorServerApp.Data;
using BlazorServerApp.Pages;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddEnvironmentVariables();

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
//builder.Services.AddSingleton<WeatherForecastService>();
builder.Services.AddSingleton(typeof(log4net.ILog),
    builder => {
        return log4net.LogManager.GetLogger(typeof(Program));
    }
);

//builder.Services.AddSingleton<TicTacToeService>();
var ticTacToeService = new TicTacToeService(builder.Configuration);
builder.Services.AddScoped<TicTacToeService>(sp => ticTacToeService);

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (!app.Environment.IsDevelopment())
if (!app.Environment.EnvironmentName.StartsWith("Development"))
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();

    app.UseHttpsRedirection();
}

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");
//app.MapFallbackToPage("/bapp");

app.Run();
