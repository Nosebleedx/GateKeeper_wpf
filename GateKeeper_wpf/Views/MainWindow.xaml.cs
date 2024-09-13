using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using GateKeeper_wpf.Infrasctructure.Helpers;
using GateKeeper_wpf.Infrasctructure;
using GateKeeper_wpf.Models;

namespace GateKeeper_wpf
{
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
            UserManager.LoadUsers();
        }

        // Обработчик нажатия кнопки входа
        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            string username = txtUsername.Text;
            string password = AuthHelper.HashPassword(txtPassword.Password);  // Получение введенного пароля

            var user = UserManager.Users.FirstOrDefault(u => u.Username == username);

            if (user != null && user.Password == password)
            {
                if (user.Role == 1)  // Проверка на админа
                {
                    MessageBox.Show("Welcome, Admin!", "Authentication", MessageBoxButton.OK, MessageBoxImage.Information);
                    //OpenAdminWindow();
                }
                else
                {
                    MessageBox.Show("Login successful!", "Authentication", MessageBoxButton.OK, MessageBoxImage.Information);
                    //OpenUserWindow();
                }
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
