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
    public DoctorCreateWindow()
    {
        InitializeComponent();
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

    }

    private void txt_PhoneNuber_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
    {
        e.Handled = !Regex.IsMatch(e.Text, "^[0-9]+$");
    }
}
