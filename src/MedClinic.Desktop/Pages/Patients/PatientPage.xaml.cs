using MedClinic.Desktop.Windows.Patients;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace MedClinic.Desktop.Pages.Patients;

/// <summary>
/// Interaction logic for PatientPage.xaml
/// </summary>
public partial class PatientPage : Page
{
    public PatientPage()
    {
        InitializeComponent();
    }

    private void SearchTextBox_KeyDown(object sender, KeyEventArgs e)
    {

    }

    private void PatientCreate_Button_Click(object sender, RoutedEventArgs e)
    {
        PatientCreateWindow patientCreateWindow = new PatientCreateWindow();
        patientCreateWindow.ShowDialog();
    }
}
