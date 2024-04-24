using BlazorServerApp.Data;
using BlazorServerApp.Pages;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.Extensions.Caching;
using Microsoft.Extensions.Caching.Distributed;
using TicTacToe.Cache;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddEnvironmentVariables();

// Add services to the container.
var config = builder.Configuration;

// Add services to the container.
builder.Services.AddDistributedSqlServerCache((options) => {
    options.ConnectionString = config.GetValue<string>("TicTacToeWebApiCache", string.Empty);
    options.SchemaName = "dbo";
    options.TableName = "TicTacToeWebApiCache";
    options.DefaultSlidingExpiration = TimeSpan.FromMinutes(60);
});
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

builder.WebHost.UseStaticWebAssets();

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

int? TicTacToeWebApiCacheExpiration = config.GetValue<int>("TicTacToeWebApiCacheExpiration");

app.Lifetime.ApplicationStarted.Register(() => {

    string currentTimeUTC = DateTime.UtcNow.ToString("O");

    var options =
        new DistributedCacheEntryOptions()
        .SetSlidingExpiration(TimeSpan.FromMinutes(TicTacToeWebApiCacheExpiration.HasValue ? TicTacToeWebApiCacheExpiration.Value : 60));
    var DistCache = app.Services.GetService<IDistributedCache>();
    if (DistCache == null)
        throw new Exception("TicTacToeWebApiCache registration failed.");

    DistCache.SetCache("AppStartTimeUTC", currentTimeUTC, options);
});

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");
//app.MapFallbackToPage("/bapp");

app.Run();
