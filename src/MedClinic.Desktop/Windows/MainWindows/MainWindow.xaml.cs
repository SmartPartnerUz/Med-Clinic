using MedClinic.BusinessLogic.Services;
using MedClinic.Desktop.Pages.Dashboard;
using MedClinic.Desktop.Pages.Doctors;
using MedClinic.Desktop.Pages.Patients;
using MedClinic.Desktop.Pages.Rooms;
using MedClinic.Desktop.Windows.Notifications;
using System.Windows;
using static MedClinic.Desktop.Windows.Notifications.NotificationWindow;

namespace MedClinic.Desktop;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private readonly IDoctorService _doctorService;
    private readonly IDoctorRoomService _doctorRoomService;
    private readonly IRoleService _roleService;
    private readonly IHospitalServiceService _hospitalServiceService;
    private readonly IPositionService _positionService;
    public MainWindow(IDoctorService doctorService,
                      IDoctorRoomService doctorRoomService,
                      IRoleService roleService,
                      IHospitalServiceService hospitalServiceService,
                      IPositionService positionService)
    {
        _doctorService = doctorService;
        _doctorRoomService = doctorRoomService;
        _roleService = roleService;
        _hospitalServiceService = hospitalServiceService;
        _positionService = positionService;
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
        DoctorPage doctorPage = new DoctorPage(_doctorService,
                                               _roleService,
                                               _doctorRoomService,
                                               _hospitalServiceService,
                                               _positionService);
        PageNavigator.Content = doctorPage;
    }

    private void rbRooms_Click(object sender, RoutedEventArgs e)
    {
        RoomPage roomPage = new RoomPage();
        PageNavigator.Content = roomPage;
    }

    private void rbServices_Click(object sender, RoutedEventArgs e)
    {
        NotificationManager.ShowNotification(MessageType.Error, "Birinchi bildirishnoma!");
    }

    private void Logout_Border_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
    {

    }

}