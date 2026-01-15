using ComplianceTracker.Domain.Interfaces;
using ComplianceTracker.Infrastructure.Repositories;
using ComplianceTracker.Domain.Service;
using ComplianceTracker.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using ComplianceTracker.Domain.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Configure Database
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

if (string.IsNullOrEmpty(connectionString))
{
    throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
}

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

// Register Repositories
builder.Services.AddScoped<IContractorRepository, ContractorRepository>();
builder.Services.AddScoped<IContractorDocumentRepository, ContractorDocumentRepository>();

// Register Services
builder.Services.AddScoped<IContractorService, ContractorService>();
builder.Services.AddScoped<IDocumentService, DocumentService>();
builder.Services.AddScoped<DocumentStatusService>();

builder.Services.AddControllers();

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApi();

// Add CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        builder =>
        {
            builder.AllowAnyOrigin()
                   .AllowAnyMethod()
                   .AllowAnyHeader();
        });
});

// Logging
builder.Services.AddLogging();

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();  // Changed from UseSwagger() for .NET 10
    app.UseSwaggerUI(config =>  // Keep UseSwaggerUI for the UI
    {
        config.SwaggerEndpoint("/openapi/v1.json", "Compliance Tracker API v1");
    });
}
app.UseHttpsRedirection();

app.UseCors("AllowAll");

app.UseAuthorization();

app.MapControllers();

// Log startup information
app.Logger.LogInformation("Starting Compliance Tracker API...");
app.Logger.LogInformation($"Environment: {app.Environment.EnvironmentName}");

// Database migration
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    try
    {
        await dbContext.Database.MigrateAsync();
        app.Logger.LogInformation("Database migrated successfully");
    }
    catch (Exception ex)
    {
        app.Logger.LogError(ex, "An error occurred while migrating the database");
        throw;
    }
}

app.Logger.LogInformation("Application started successfully");

app.Run();
