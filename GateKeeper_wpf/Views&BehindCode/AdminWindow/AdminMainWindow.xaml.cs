using GateKeeper_wpf.Models;
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

namespace GateKeeper_wpf.Views_BehindCode.AdminWindow
{
    /// <summary>
    /// Логика взаимодействия для AdminMainWindow.xaml
    /// </summary>
    public partial class AdminMainWindow : Window
    {
        private User adminUser;

        public AdminMainWindow(User user)
        {
            this.adminUser = user;
            InitializeComponent();
        }

        private void btnUserList_Click(object sender, RoutedEventArgs e)
        {
            mainContentFrame.Navigate(new UserManagementPage());  // Это страница списка пользователей
        }

        private void btnAddUser_Click(object sender, RoutedEventArgs e)
        {
            mainContentFrame.Navigate(new AddUserPage());  // Это страница добавления пользователя
        }

        private void btnChangePassword_Click(object sender, RoutedEventArgs e)
        {
            mainContentFrame.Navigate(new ChangePasswordPage(adminUser));  // Это страница смены пароля
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
