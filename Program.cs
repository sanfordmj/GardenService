using GardenService;
using Microsoft.EntityFrameworkCore;
using GardenService.Extensions;
using GardenService.Configs;
using GardenService.Helpers;
using Newtonsoft.Json;
using System.Text.Json;
using NLog;
using NLog.Web;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


//builder.Configuration
// .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
// .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json", true, true);

//string dbConn = builder.Configuration.GetSection("ConnectionStrings").GetSection("GardenConnectionSqlite").Value;

builder.Services.AddDbContext<GardenServicesDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("GardenConnectionSqlite")));

var authenticationConfiguration = new AuthenticationConfiguration();
builder.Configuration.GetSection(nameof(AuthenticationConfiguration)).Bind("AuthenticationConfiguration", authenticationConfiguration);

builder.Services.AddAuthentication("Basic")
        .AddScheme<BasicAuthenticationOptions, CustomAuthenticationHandler>("Basic", null);

builder.Logging.ClearProviders();
builder.Logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
builder.Host.UseNLog();

builder.Services.AddSingleton<ICustomAuthenticationManager, CustomAuthenticationManager>();

builder.Services.AddMvc().AddJsonOptions(x =>
{
    x.JsonSerializerOptions.PropertyNamingPolicy = null;
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

/*
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var db = services.GetRequiredService<GardenServicesDbContext>();
        //db.Database.Migrate();
        
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "Database Creation/Migrations failed!");
    }
}
*/


app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers().RequireAuthorization();

app.Run();
