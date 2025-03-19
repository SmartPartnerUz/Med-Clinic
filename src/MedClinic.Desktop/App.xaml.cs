using MedClinic.BusinessLogic.Configurations;
using MedClinic.DataAccess.Data;
using MedClinic.DataAccess.Repositories;
using MedClinic.Desktop.Windows;
using MedClinic.Desktop.Windows.Login;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Configuration;
using System.Data;
using System.Windows;

namespace MedClinic.Desktop;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    private readonly IHost _host;
    public static IServiceProvider ServiceProvider { get; private set; }

    public App()
    {
        _host = Host.CreateDefaultBuilder()
            .ConfigureAppConfiguration((context, config) =>
            {
                // Load appsettings.json
                config.SetBasePath(AppDomain.CurrentDomain.BaseDirectory);
                config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            })
            .ConfigureServices((context, services) =>
            {
                // Pass IConfiguration to AddBusinessLogicServices
                services.AddBusinessLogicServices(context.Configuration);
            })
            .Build();

        ServiceProvider = _host.Services;
    }

    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);
        // Use the factory method instead of direct resolution
        var loginWindow = WindowFactory.CreateLoginWindow();
        loginWindow.Show();
    }

    protected override void OnExit(ExitEventArgs e)
    {
        _host.Dispose();
        base.OnExit(e);
    }
}
