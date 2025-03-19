using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Controls;
using System.Windows.Input;
using static System.Net.Mime.MediaTypeNames;

namespace MedClinic.Desktop.Pages.Rooms;

/// <summary>
/// Interaction logic for RoomCreatePage.xaml
/// </summary>
public partial class RoomCreatePage : Page
{
    public RoomCreatePage()
    {
        InitializeComponent();
    }

    private void tb_PreviewTextInput(object sender, TextCompositionEventArgs e)
    {
        e.Handled = !Regex.IsMatch(e.Text, "^[0-9]+$");
    }

    private void Save_Button_Click(object sender, System.Windows.RoutedEventArgs e)
    {

    }
}
