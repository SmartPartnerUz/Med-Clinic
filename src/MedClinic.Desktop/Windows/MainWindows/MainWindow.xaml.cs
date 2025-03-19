using MedClinic.Desktop.Pages.Dashboard;
using MedClinic.Desktop.Pages.Doctors;
using MedClinic.Desktop.Pages.Patients;
using MedClinic.Desktop.Pages.Rooms;
using MedClinic.Desktop.Windows.Notifications;
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

    private void Window_Loaded(object sender, RoutedEventArgs e)
    {
        
    }

    private void btnClose_Click(object sender, RoutedEventArgs e)
    {
        Application.Current.Shutdown();
    }

    private void rbDashboard_Click(object sender, RoutedEventArgs e)
    {
        DashboardPage dashboardPage = new DashboardPage();
        PageNavigator.Content = dashboardPage;
    }

    private void rbPatient_Click(object sender, RoutedEventArgs e)
    {
        PatientPage patientPage = new PatientPage();
        PageNavigator.Content = patientPage;
    }

    private void rbDoctors_Click(object sender, RoutedEventArgs e)
    {
        DoctorPage doctorPage = new DoctorPage();
        PageNavigator.Content = doctorPage;
    }

    private void rbRooms_Click(object sender, RoutedEventArgs e)
    {
        RoomPage roomPage = new RoomPage();
        PageNavigator.Content = roomPage;
    }

    private void rbServices_Click(object sender, RoutedEventArgs e)
    {
        NotificationManager.ShowNotification("Birinchi bildirishnoma!");
    }

    private void Logout_Border_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
    {

    }

}