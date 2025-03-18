using MedClinic.Desktop.Components.Patients;
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

    private void Page_Loaded(object sender, RoutedEventArgs e)
    {
        for (int i = 0; i < 30; i++)
        {
            PatientComponents patientComponents = new PatientComponents();
            st_Patient.Children.Add(patientComponents);
        }
    }
}
