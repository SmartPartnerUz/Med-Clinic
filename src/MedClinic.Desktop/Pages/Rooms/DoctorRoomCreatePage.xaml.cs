using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;

namespace MedClinic.Desktop.Pages.Rooms;

/// <summary>
/// Interaction logic for DoctorRoomCreatePage.xaml
/// </summary>
public partial class DoctorRoomCreatePage : Page
{
    public DoctorRoomCreatePage()
    {
        InitializeComponent();
    }

    private void Page_Loaded(object sender, RoutedEventArgs e)
    {

    }

    private void tb_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
    {
        e.Handled = !Regex.IsMatch(e.Text, "^[0-9]+$");
    }

    private void Save_Button_Click(object sender, RoutedEventArgs e)
    {

    }
}
