using MedClinic.BusinessLogic.Services;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Interop;
using static MedClinic.Desktop.Windows.BlurWindow.BlurEffect;

namespace MedClinic.Desktop.Windows.Doctors;

/// <summary>
/// Interaction logic for DoctorCreateWindow.xaml
/// </summary>
public partial class DoctorCreateWindow : Window
{
    private readonly IDoctorRoomService _doctorRoomService;
    private readonly IRoleService _roleService;
    private readonly IPositionService _positionService;
    private readonly IHospitalServiceService _hospitalServiceService;
    private readonly IDoctorService _doctorService;
    public DoctorCreateWindow(IDoctorRoomService doctorRoomService,
                              IRoleService roleService,
                              IPositionService positionService,
                              IHospitalServiceService hospitalServiceService,
                              IDoctorService doctorService)
    {
        InitializeComponent();
        _doctorRoomService = doctorRoomService;
        _roleService = roleService;
        _positionService = positionService;
        _hospitalServiceService = hospitalServiceService;
        _doctorService = doctorService;
    }

    [DllImport("user32.dll")]
    internal static extern int SetWindowCompositionAttribute(IntPtr hwnd, ref WindowCompositionAttributeData data);
    internal void EnableBlur()
    {
        var windowHelper = new WindowInteropHelper(this);

        var accent = new AccentPolicy
        {
            AccentState = AccentState.ACCENT_ENABLE_BLURBEHIND,
            AccentFlags = 2,
            GradientColor = unchecked((int)0x80000000),
            AnimationId = 0
        };
        accent.AccentState = AccentState.ACCENT_ENABLE_BLURBEHIND;

        var accentStructSize = Marshal.SizeOf(accent);

        var accentPtr = Marshal.AllocHGlobal(accentStructSize);
        Marshal.StructureToPtr(accent, accentPtr, false);

        var data = new WindowCompositionAttributeData();
        data.Attribute = WindowCompositionAttribute.WCA_ACCENT_POLICY;
        data.SizeOfData = accentStructSize;
        data.Data = accentPtr;

        SetWindowCompositionAttribute(windowHelper.Handle, ref data);

        Marshal.FreeHGlobal(accentPtr);
    }

    private void Window_Loaded(object sender, RoutedEventArgs e)
    {
        EnableBlur();
        var disrupt = new DoctorRoomSortFilterOptions();
        var rooms = _doctorRoomService.GetAllDoctorRooms(disrupt);
        doctor_Room.ItemsSource = rooms.Items;
        doctor_Room.DisplayMemberPath = "Number";
        doctor_Room.SelectedValuePath = "Id";

        var options = new RoleSortFilterOptions();
        var roles = _roleService.GetAllRoles(options);
        //doctor_Role.Items.Clear();
        doctor_Role.ItemsSource = roles.Items;
        doctor_Role.DisplayMemberPath = "Name";
        doctor_Role.SelectedValuePath = "Id";

        var positionOptions = new PositionSortFilterOptions();
        var positions = _positionService.GetAllPositions(positionOptions);
        doctor_Position.ItemsSource = positions.Items;
        doctor_Position.DisplayMemberPath = "Name";
        doctor_Position.SelectedValuePath = "Id";

        var hospitalOptions = new HospitalServiceSortFilterOptions();
        var hospitals = _hospitalServiceService.GetAllHospitalServices(hospitalOptions);
        doctor_Service.ItemsSource = hospitals.Items;
        doctor_Service.DisplayMemberPath = "Name";
        doctor_Service.SelectedValuePath = "Id";
    }

    private void Close_Button_Click(object sender, RoutedEventArgs e)
    {
        this.Close();
    }

    private void doctor_Image_Border_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
    {

    }

    private void doctor_Image_Border_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
    {

    }

    private void doctor_Image_Border_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
    {

    }

    private void Create_Button_Click(object sender, RoutedEventArgs e)
    {
        double salary;
        int bedPercentage;
        MessageBox.Show(txt_Firstname.Text);
        MessageBox.Show(txt_Lastname.Text);
        MessageBox.Show(txt_PhoneNuber.Text);
        MessageBox.Show(txt_Persentage.Text);
        if (!double.TryParse(txt_Salary.Text, out salary))
        {
            MessageBox.Show("Please enter a valid salary.");
            // show txt_Salary.Text with MessageBox

            return;
        }

        if (!int.TryParse(txt_Persentage.Text, out bedPercentage))
        {
            MessageBox.Show("Please enter a valid percentage.");
            return;
        }
        var newDoctor = new AddDoctorDto()
        {
            FirstName = txt_Firstname.Text,
            LastName = txt_Lastname.Text,
            PhoneNumber = txt_PhoneNuber.Text,
            Salary = Convert.ToDouble(txt_Salary.Text),
            DoctorRoomId = (Guid)doctor_Room.SelectedValue,
            RoleId = (Guid)doctor_Role.SelectedValue,
            PositionId = (Guid)doctor_Position.SelectedValue,
            HospitalServiceId = (Guid)doctor_Service.SelectedValue,
            BedPercentage = Convert.ToInt32(txt_Persentage.Text)
        };

        _doctorService.CreateDoctor(newDoctor);
        this.Close();
    }

    private void txt_PhoneNuber_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
    {
        e.Handled = !Regex.IsMatch(e.Text, "^[0-9]+$");
    }
}
