using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Effects;

namespace MedClinic.Desktop.Components;

/// <summary>
/// Interaction logic for RoomComponents.xaml
/// </summary>
public partial class RoomComponents : UserControl
{
    public RoomComponents()
    {
        InitializeComponent();
    }

    public void SetData(int number)
    {
        tb_Number.Text = number.ToString();

        Color color = (Color)ColorConverter.ConvertFromString("#E1AA3C");

        if (number == 5 || number == 10 || number == 12 || number == 15 || number == 25)
        {
            (Room_Border.Effect as DropShadowEffect)!.Color = color;
        }
    }
}
