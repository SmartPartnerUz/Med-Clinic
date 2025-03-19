using MedClinic.Desktop.Components;
using MedClinic.Desktop.Windows.Rooms;
using System.Windows.Controls;
using System.Windows.Input;

namespace MedClinic.Desktop.Pages.Rooms;

/// <summary>
/// Interaction logic for RoomPage.xaml
/// </summary>
public partial class RoomPage : Page
{
    public RoomPage()
    {
        InitializeComponent();
    }

    private void SearchTextBox_KeyDown(object sender, KeyEventArgs e)
    {

    }

    private void RoomCreate_Button_Click(object sender, System.Windows.RoutedEventArgs e)
    {
        RoomCreateWindow roomCreateWindow = new RoomCreateWindow();
        roomCreateWindow.ShowDialog();
    }

    private void Page_Loaded(object sender, System.Windows.RoutedEventArgs e)
    {
        for (int i = 1; i < 30; i++)
        {
            RoomComponents roomComponents = new RoomComponents();
            roomComponents.SetData(i);
            st_Rooms.Children.Add(roomComponents);
        }
    }
}
