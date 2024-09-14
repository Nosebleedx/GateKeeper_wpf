using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using GateKeeper_wpf.Infrasctructure.Helpers;
using GateKeeper_wpf.Infrasctructure;
using GateKeeper_wpf.Models;
using GateKeeper_wpf.Views_BehindCode.AdminWindow;
using GateKeeper_wpf.Views_BehindCode.UserWindow;

namespace GateKeeper_wpf
{
    public partial class MainWindow : Window
    {

        private int failedAttempts = 0;  // Счётчик неудачных попыток


        public MainWindow()
        {
            InitializeComponent();
            UserManager.LoadUsers();
        }

        // Обработчик нажатия кнопки входа
        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            string username = txtUsername.Text;
            string password = txtPassword.Password;  // Получение введенного пароля

            var user = UserManager.Users.FirstOrDefault(u => u.Username == username);

            if (user != null && user.Password == password)
            {
                failedAttempts = 0;
                OpenLoggedWindow(user);
                this.Close();
            }
            else
            {
                failedAttempts++; 
                MessageBox.Show($"Authentication failed. Wrong username or password. Attempts left: {3 - failedAttempts}", "Authentication Failed", MessageBoxButton.OK, MessageBoxImage.Warning);

                if (failedAttempts >= 3)
                {
                    MessageBox.Show("Too many failed attempts. The application will now close.", "Authentication Failed", MessageBoxButton.OK, MessageBoxImage.Warning);
                    Application.Current.Shutdown();
                }
            }
        }

        private void OpenLoggedWindow(User currentUser)
        {
            if (currentUser.Role == 1)
            {
                MessageBox.Show("Welcome, Admin!", "Authentication", MessageBoxButton.OK, MessageBoxImage.Information);
                AdminMainWindow adminWindow = new AdminMainWindow(currentUser);
                adminWindow.Show();
            }
            else
            {
                MessageBox.Show("Login successful!", "Authentication", MessageBoxButton.OK, MessageBoxImage.Information);
                UserMainWindow userWindow = new UserMainWindow(currentUser);
                userWindow.Show();
            }
        }
        // Метод проверки логина и пароля
        private bool CheckUserCredentials(string username, string hashedPassword)
        {
            var user = UserManager.Users.FirstOrDefault(u => u.Username == username);
            if (user != null && user.Password == hashedPassword)
            {
                return true;  // Вход успешен
            }

            return false;  // Логин или пароль неверны
        }
    }

    
}
