using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using nhlstatswebportal_blazor.Data;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);
var configBuilder = new ConfigurationBuilder();

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddSingleton<WeatherForecastService>();

var app = builder.Build();

configBuilder.AddAzureAppConfiguration(Environment.GetEnvironmentVariable("NHLSTATS_APPCONFIG_CONNECTIONSTRING"));

var config = configBuilder.Build();

//************************************************************************
// Get configuration data from Azure App Configuration
// Database:
string keyName = "nhlstats:database:azure";
string connectionString = config[keyName];

// App Insights:
keyName = "nhlstats:monitor:appinsights";
string telemetryKey = config[keyName];
//************************************************************************

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
