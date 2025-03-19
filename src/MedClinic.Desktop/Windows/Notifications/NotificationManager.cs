using System.Windows;
using System.Windows.Media.Animation;

namespace MedClinic.Desktop.Windows.Notifications
{
    public static class NotificationManager
    {
        private static readonly List<NotificationWindow> ActiveNotifications = new();
        private const int MaxNotifications = 3;
        private const int NotificationHeight = 80; 
        private const int Margin = 10;

        public static void ShowNotification(string message, int duration = 3000)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                if (ActiveNotifications.Count >= MaxNotifications)
                {
                    var oldest = ActiveNotifications.First();
                    oldest.Close();
                }

                var notification = new NotificationWindow(message, duration);
                PositionNotification(notification);
                notification.Show();

                ActiveNotifications.Add(notification);
                notification.Closed += (s, e) =>
                {
                    ActiveNotifications.Remove(notification);
                    UpdatePositions();
                };
            });
        }

        private static void PositionNotification(NotificationWindow notification)
        {
            double startX = SystemParameters.WorkArea.Width - notification.Width - 10;
            double startY = SystemParameters.WorkArea.Height - NotificationHeight - Margin;

            foreach (var existingNotification in ActiveNotifications)
            {
                startY -= NotificationHeight + Margin;
            }

            notification.Left = startX;
            notification.Top = startY;
        }

        private static void UpdatePositions()
        {
            double startY = SystemParameters.WorkArea.Height - 90;

            foreach (var notification in ActiveNotifications)
            {
                DoubleAnimation moveAnim = new DoubleAnimation
                {
                    To = startY,
                    Duration = TimeSpan.FromMilliseconds(300),
                    DecelerationRatio = 0.5
                };

                notification.BeginAnimation(Window.TopProperty, moveAnim);
                startY -= NotificationHeight + Margin;
            }
        }
    }
}
