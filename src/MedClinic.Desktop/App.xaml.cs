using MedClinic.DataAccess.Data;
using MedClinic.DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
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
    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);

        string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

        var services = new ServiceCollection();

        services.AddDbContext<MedClinic.DataAccess.Data.AppDbContext>(options =>
            options.UseNpgsql(connectionString));

        var serviceProvider = services.BuildServiceProvider();
    }
}

