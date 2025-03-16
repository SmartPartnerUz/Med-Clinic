using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace MedClinic.Desktop.Components.Doctors;

/// <summary>
/// Interaction logic for DoctorComponents.xaml
/// </summary>
public partial class DoctorComponent : UserControl
{
    public DoctorComponent()
    {
        InitializeComponent();
    }

    private void DoctorImage_MouseEnter(object sender, MouseEventArgs e)
    {
        DoctorImage.BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#329DFF"));
    }

    private void DoctorImage_MouseLeave(object sender, MouseEventArgs e)
    {
        DoctorImage.BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("Transparent"));
    }

    private void DoctorImage_MouseDown(object sender, MouseButtonEventArgs e)
    {

    }

    private void deletebtn_Click(object sender, System.Windows.RoutedEventArgs e)
    {

    }

    private void btnManege_Click(object sender, System.Windows.RoutedEventArgs e)
    {

    }
}
