using MedClinic.BusinessLogic.Services;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace MedClinic.Desktop.Windows.Login;

/// <summary>
/// Interaction logic for LoginWindow.xaml
/// </summary>
public partial class LoginWindow : Window
{
    private readonly IUserService _userService;

    public LoginWindow(IUserService userService)
    {
        InitializeComponent();
        _userService = userService;
    }

    private void btnClose_Click(object sender, RoutedEventArgs e)
    {
        Application.Current.Shutdown();
    }

    private void Label_MouseEnter(object sender, MouseEventArgs e)
    {
        lblForgotPassword.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#329DFF"));
    }

    private void Label_MouseLeave(object sender, MouseEventArgs e)
    {
        lblForgotPassword.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("Black"));
    }

    private void Border_MouseEnter(object sender, MouseEventArgs e)
    {
        Phoneborder.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#329DFF"));
    }

    private void Border_MouseLeave(object sender, MouseEventArgs e)
    {
        Phoneborder.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#979797"));
    }

    private void Border_MouseEnter_1(object sender, MouseEventArgs e)
    {
        Parolborder.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#329DFF"));
    }

    private void Border_MouseLeave_1(object sender, MouseEventArgs e)
    {
        Parolborder.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#979797"));
    }

    private void showPassword_Click(object sender, RoutedEventArgs e)
    {
        if (textboxParolText.Visibility == Visibility.Collapsed)
        {
            showPassword.Style = (Style)FindResource("showPasswordCrosButton");
            textboxParolText.Text = textboxParol.Password;
            textboxParol.Visibility = Visibility.Collapsed;
            textboxParolText.Visibility = Visibility.Visible;
        }
        else
        {
            showPassword.Style = (Style)FindResource("showPasswordButton");
            textboxParol.Password = textboxParolText.Text;
            textboxParolText.Visibility = Visibility.Collapsed;
            textboxParol.Visibility = Visibility.Visible;
        }
    }

    private async void LoginBtn_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            //var userDto = new AddUserDto
            //{
            //    FirstName = "Akrom",
            //    LastName = "Saminov",
            //    PhoneNumber = textboxPhone.Text
            //};

            //await _userService.CreateUser(userDto);

            //MessageBox.Show("User registered successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

            MainWindow mainWindow = WindowFactory.CreateMainWindow();
            mainWindow.Show();
            this.Close();
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error: {ex.Message}", "Registration Failed", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }

    private void Label_MouseDoubleClick(object sender, MouseButtonEventArgs e)
    {

    }

    private void textboxPhone_PreviewTextInput(object sender, TextCompositionEventArgs e)
    {
        e.Handled = !Regex.IsMatch(e.Text, "^[0-9]+$");
    }
}
