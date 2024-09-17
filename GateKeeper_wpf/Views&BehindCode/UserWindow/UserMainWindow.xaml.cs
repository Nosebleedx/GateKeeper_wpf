using GateKeeper_wpf.Models;
using GateKeeper_wpf.Views_BehindCode.AdminWindow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

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
