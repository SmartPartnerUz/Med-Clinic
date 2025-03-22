using MedClinic.BusinessLogic.Services;
using MedClinic.Desktop.Components.Doctors;
using MedClinic.Desktop.Windows.Doctors;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace MedClinic.Desktop.Pages.Doctors;

/// <summary>
/// Interaction logic for DoctorPage.xaml
/// </summary>
public partial class DoctorPage : Page
{
    private readonly IDoctorService _doctorService;
    private readonly IDoctorRoomService _doctorRoomService;
    private readonly IRoleService _roleService;
    private readonly IHospitalServiceService _hospitalServiceService;
    private readonly IPositionService _positionService;
    public DoctorPage(IDoctorService doctorService,
                      IRoleService roleService,
                      IDoctorRoomService doctorRoomService,
                      IHospitalServiceService hospitalServiceService,
                      IPositionService positionService)
    {
        InitializeComponent();
        _doctorService = doctorService;
        _doctorRoomService = doctorRoomService;
        _roleService = roleService;
        _hospitalServiceService = hospitalServiceService;
        _positionService = positionService;
    }

    private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
    {

    }

    private void TextBox_PreviewKeyDown(object sender, KeyEventArgs e)
    {

    }

    private void DoctorCreate_Button_Click(object sender, RoutedEventArgs e)
    {
        DoctorCreateWindow doctorCreateWindow = new DoctorCreateWindow(_doctorRoomService,
                                                                       _roleService,
                                                                       _positionService,
                                                                       _hospitalServiceService,
                                                                       _doctorService);
        doctorCreateWindow.ShowDialog();
    }

    private void Page_Loaded(object sender, RoutedEventArgs e)
    {
        var options = new DoctorSortFilterOptions();
        var doctors = _doctorService.GetAllDoctors(options);
        foreach (var doctor in doctors.Items)
        {
            var doctorComponent = new DoctorComponent
            {
                DoctorName = new Label { Content = doctor.User.FirstName + " " + doctor.User.LastName },
                DoctorPersonality = new Label { Content = doctor.Position.Name }
            };
            wrpDoctors.Children.Add(doctorComponent);
        }
    }

    private void SearchTextBox_KeyDown(object sender, KeyEventArgs e)
    {

    }
}
