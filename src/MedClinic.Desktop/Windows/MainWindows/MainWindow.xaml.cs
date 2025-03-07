using System.Windows;

namespace MedClinic.Desktop;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    private void btnClose_Click(object sender, RoutedEventArgs e)
    {
        Application.Current.Shutdown();
    }

    private void rbDashboard_Click(object sender, RoutedEventArgs e)
    {

    }

    private void rbPatient_Click(object sender, RoutedEventArgs e)
    {

    }

    private void rbDoctors_Click(object sender, RoutedEventArgs e)
    {

    }

    private void rbRooms_Click(object sender, RoutedEventArgs e)
    {

    }

    private void Window_Loaded(object sender, RoutedEventArgs e)
    {

    }

    private void Logout_Border_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
    {

    }
}