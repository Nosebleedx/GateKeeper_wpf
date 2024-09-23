using GateKeeper_wpf.Models;
using GateKeeper_wpf.Views_BehindCode.AdminWindow;
using System.Windows;

namespace GateKeeper_wpf.Views_BehindCode.UserWindow
{
    /// <summary>
    /// Логика взаимодействия для UserMainWindow.xaml
    /// </summary>
    public partial class UserMainWindow : Window
    {
        private User defaultUser;


        public UserMainWindow(User user)
        {
            defaultUser = user;
            InitializeComponent();
        }

        private void btnChangePassword_Click(object sender, RoutedEventArgs e)
        {
            mainContentFrame.Navigate(new ChangePasswordPage(defaultUser));
        }

        private void btnAboutButton_Click(object obj, RoutedEventArgs e)
        {
            mainContentFrame.Navigate(new AboutWindow());
        }

        private void btnLogout_Click(object sender, RoutedEventArgs e)
        {
            MainWindow loginWindow = new MainWindow();
            loginWindow.Show();
            GetWindow(this).Close();
        }
    }
}
