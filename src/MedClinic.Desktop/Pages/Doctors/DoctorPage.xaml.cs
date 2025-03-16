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
    public DoctorPage()
    {
        InitializeComponent();
    }

    private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
    {

    }

    private void TextBox_PreviewKeyDown(object sender, KeyEventArgs e)
    {

    }

    private void DoctorCreate_Button_Click(object sender, RoutedEventArgs e)
    {
        DoctorCreateWindow doctorCreateWindow = new DoctorCreateWindow();
        doctorCreateWindow.ShowDialog();
    }

    private void Page_Loaded(object sender, RoutedEventArgs e)
    {
        for (int i = 0; i < 20; i++)
        {
            DoctorComponent doctorComponent = new DoctorComponent();
            wrpDoctors.Children.Add(doctorComponent);
        }
    }

    private void SearchTextBox_KeyDown(object sender, KeyEventArgs e)
    {

    }
}
