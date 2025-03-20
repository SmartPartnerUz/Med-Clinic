using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;
using System.Diagnostics;

namespace MedClinic.BusinessLogic.Configurations;

public static class LoggingConfiguration
{
    public static IServiceCollection AddLoggingConfiguration(this IServiceCollection services)
    {
        try
        {
            string currentDir = AppDomain.CurrentDomain.BaseDirectory;

            // Locate "Med-Clinic" as the solution root
            DirectoryInfo? directory = new DirectoryInfo(currentDir);
            while (directory?.Parent != null && directory.Name != "Med-Clinic")
                directory = directory.Parent;

            // If found, use structured path; otherwise, default to logs folder
            string logDir = directory != null
                ? Path.Combine(directory.FullName, "MedClinic.BusinessLogic", "Configurations", "Logging", "logs")
                : Path.Combine(Directory.GetCurrentDirectory(), "logs");

            // Ensure the logs directory exists
            Directory.CreateDirectory(logDir);

            // Set up Serilog
            string logPath = Path.Combine(logDir, "backend-log.txt");
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Information()
                .WriteTo.File(logPath, rollingInterval: RollingInterval.Day)
                .CreateLogger();

            Debug.WriteLine("Serilog configured successfully");

            // Add Serilog to logging services
            services.AddLogging(loggingBuilder =>
            {
                loggingBuilder.ClearProviders();
                loggingBuilder.AddSerilog(dispose: true);
            });

            return services;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Logging configuration failed: {ex.Message}");
            throw;
        }
    }
}
