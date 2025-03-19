using System.Windows;
using System.Windows.Media.Animation;
using System.Windows.Threading;

namespace MedClinic.Desktop.Windows.Notifications;

/// <summary>
/// Interaction logic for NotificationWindow.xaml
/// </summary>
public partial class NotificationWindow : Window
{
    private DispatcherTimer timer;

    public NotificationWindow(string message, int duration = 3000)
    {
        InitializeComponent();
        MessageText.Text = message;

        var showAnim = (Storyboard)FindResource("ShowAnimation");
        BeginStoryboard(showAnim);

        timer = new DispatcherTimer { Interval = TimeSpan.FromMilliseconds(duration) };
        timer.Tick += (s, e) => CloseNotification();
        timer.Start();
    }

    private void CloseNotification()
    {
        timer.Stop();

        var closeAnim = (Storyboard)FindResource("CloseAnimation");
        closeAnim.Completed += (s, e) => this.Close();
        BeginStoryboard(closeAnim);
    }

    private void CloseButton_Click(object sender, RoutedEventArgs e)
    {
        CloseNotification();
    }
}
