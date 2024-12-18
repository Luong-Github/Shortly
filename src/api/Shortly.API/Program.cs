using Microsoft.EntityFrameworkCore;
using Serilog;
using Shortly.API.Extensions;
using Shortly.API.Middlewares;
using Shortly.Application.Extensions;
using Shortly.Persistence.Contexts;
using Shortly.Persistence.Extensions;
using Shortly.Persistence.Options;
using static Shortly.Infrastructure.InfrastructureStartup;

var builder = WebApplication.CreateBuilder(args);

// Configuration setup
var configuration = builder.Configuration;

// Serilog setup
Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(configuration)
    .Enrich.FromLogContext()
    .Enrich.WithMachineName()
    .WriteTo.Console()
    .CreateLogger();

builder.Host.UseSerilog();

// Register services
builder.Services.AddHealthChecks();
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.ConfigureServices();
builder.Services.AddInfrastructureConfigureServices();
builder.Services.ApplicationServiceConfigure();
builder.Services.AddSqlServerPersistence(configuration);
builder.Services.PersistenceConfigureServices();

var app = builder.Build();

// Exception handling middleware
app.UseExceptionHandler(opt =>
{
    // Ensure global exception logging happens
});

app.UseHealthChecks("/health");
app.UseSerilogRequestLogging();

using (var scope = app.Services.CreateScope())
{
    Log.Logger.Information("Start database migration...");
    try
    {
        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        context.Database.Migrate();
        Log.Logger.Information("Database migration completed successfully.");
    }
    catch (Exception ex)
    {
        Log.Logger.Fatal(ex, "Database migration failed.");
        throw;
    }
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();

app.Lifetime.ApplicationStopping.Register(() =>
    Log.Logger.Information("Application is shutting down..."));

app.Run();
