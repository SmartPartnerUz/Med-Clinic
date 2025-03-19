using System.Runtime.InteropServices;
using System.Windows;
using static MedClinic.Desktop.Windows.BlurWindow.BlurEffect;
using System.Windows.Interop;
using MedClinic.Desktop.Pages.Rooms;

namespace MedClinic.Desktop.Windows.Rooms;

/// <summary>
/// Interaction logic for RoomCreateWindow.xaml
/// </summary>
public partial class RoomCreateWindow : Window
{
    public RoomCreateWindow()
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
        RoomCreatePage roomCreatePage = new RoomCreatePage();
        PageNavigator.Content = roomCreatePage;
    }

    private void Close_Button_Click(object sender, RoutedEventArgs e)
    {
        this.Close();
    }

    private void rb_Room_Click(object sender, RoutedEventArgs e)
    {
        RoomCreatePage roomCreatePage = new RoomCreatePage();
        PageNavigator.Content = roomCreatePage;
    }

    private void rb_DoctorRoom_Click(object sender, RoutedEventArgs e)
    {

    }

    private void rb_Bed_Click(object sender, RoutedEventArgs e)
    {

    }
}
