using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
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

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            string username = txtUsername.Text;
            string password = txtPassword.Password;

            var user = UserManager.Users.FirstOrDefault(u => u.Username == username);
            
            if (user != null && user.Password == password && !user.IsBlocked)
            {
                failedAttempts = 0;
                OpenLoggedWindow(user);
                this.Close();
            }
            else
            {
                failedAttempts++;
                if(user.IsBlocked)
                {
                    MessageBox.Show($"Аутентификация провалена. Пользователь заблокирован. Попыток осталось: {3 - failedAttempts}", "Authentication failed", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                else
                {
                    MessageBox.Show($"Аутентификация провалена. Неверное имя пользователя или пароль. Попыток осталось: {3 - failedAttempts}", "Authentication failed", MessageBoxButton.OK, MessageBoxImage.Warning);
                }

                if (failedAttempts >= 3)
                {
                    MessageBox.Show("Слишком много попыток. Отказано в доступе, закрытие программы", "Authentication failed", MessageBoxButton.OK, MessageBoxImage.Warning);
                    Application.Current.Shutdown();
                }
            }
        }

        private void OpenLoggedWindow(User currentUser)
        {
            if (currentUser.Role == 1)
            {
                MessageBox.Show("Добро пожаловать, Админ!", "Authentication", MessageBoxButton.OK, MessageBoxImage.Information);
                AdminMainWindow adminWindow = new AdminMainWindow(currentUser);
                adminWindow.Show();
            }
            else
            {
                MessageBox.Show("Успешный вход!", "Authentication", MessageBoxButton.OK, MessageBoxImage.Information);
                UserMainWindow userWindow = new UserMainWindow(currentUser);
                userWindow.Show();
            }
        }
        private bool CheckUserCredentials(string username, string hashedPassword)
        {
            var user = UserManager.Users.FirstOrDefault(u => u.Username == username);
            if (user != null && user.Password == hashedPassword)
            {
                return true;
            }

            return false;
        }
    }

    
}
