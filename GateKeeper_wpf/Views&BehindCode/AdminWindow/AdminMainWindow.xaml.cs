﻿using GateKeeper_wpf.Models;
using System.Windows;

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
            mainContentFrame.Navigate(new UserManagementPage());
        }

        private void btnAddUser_Click(object sender, RoutedEventArgs e)
        {
            mainContentFrame.Navigate(new AddUserPage());
        }

        private void btnChangePassword_Click(object sender, RoutedEventArgs e)
        {
            mainContentFrame.Navigate(new ChangePasswordPage(adminUser));
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
