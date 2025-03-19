using MedClinic.BusinessLogic.Services;
using Microsoft.Extensions.DependencyInjection;

namespace MedClinic.Desktop.Windows;

public static class WindowFactory
{
    public static Login.LoginWindow CreateLoginWindow()
    {
        // Get IUserService from your DI container
        var userService = App.ServiceProvider.GetRequiredService<IUserService>();
        return new Login.LoginWindow(userService);
    }

    // You can add factory methods for other windows too
    public static MainWindow CreateMainWindow()
    {
        // If MainWindow has dependencies, resolve them here
        return new MainWindow();
    }
}