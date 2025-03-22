using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Threading;

namespace MedClinic.Desktop.Windows.Notifications;

/// <summary>
/// Interaction logic for NotificationWindow.xaml
/// </summary>
public partial class NotificationWindow : Window
{
    private DispatcherTimer timer;

    public enum MessageType
    {
        Info,
        Success,
        Warning,
        Error,
    }

    public NotificationWindow(MessageType Type, string message, int duration = 3000)
    {
        InitializeComponent();
        MessageText.Text = message;

        switch (Type)
        {
            case MessageType.Info:
                    Header_Border.Background = new SolidColorBrush(Color.FromRgb(13, 110, 253));
                break;
            case MessageType.Success:
                    Header_Border.Background = new SolidColorBrush(Color.FromRgb(0, 128, 0));
                break;
            case MessageType.Warning:
                    Header_Border.Background = new SolidColorBrush(Color.FromRgb(251, 255, 0));
                break;
            case MessageType.Error:
                    Header_Border.Background = new SolidColorBrush(Color.FromRgb(255, 0, 0));
                break;
        }

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

    private void Close_Button_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
    {
        CloseNotification();
    }
}
