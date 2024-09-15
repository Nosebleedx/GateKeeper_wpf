using GateKeeper_wpf.Infrasctructure;
using GateKeeper_wpf.Models;
using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;

namespace GateKeeper_wpf.Views_BehindCode.AdminWindow
{
    public partial class AddUserPage : Page
    {
        public AddUserPage()
        {
            InitializeComponent();
        }

        private void txtPassword_PasswordChanged(object sender, RoutedEventArgs e)
        {
            var password = txtPassword.Password;

            lblLength.Foreground = password.Length >= GetMinPasswordLength() ? System.Windows.Media.Brushes.Green : System.Windows.Media.Brushes.Red;
            lblUppercase.Foreground = Regex.IsMatch(password, @"^(?=.*[a-z])(?=.*[A-Z]).+$") ? System.Windows.Media.Brushes.Green : System.Windows.Media.Brushes.Red;
            lblRepeats.Foreground = password.Distinct().Count() == password.Length ? System.Windows.Media.Brushes.Green : System.Windows.Media.Brushes.Red;
        }

        private int GetMinPasswordLength()
        {
            return 8;
        }

        private void btnAddUser_Click(object sender, RoutedEventArgs e)
        {
            string username = txtUsername.Text;
            string password = txtPassword.Password;

            if (UserManager.Users.Any(u => u.Username == username))
            {
                MessageBox.Show("Пользователь с таким именем уже существует.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (!IsPasswordValid(password))
            {
                MessageBox.Show("Пароль не соответствует требованиям.", "Ошибка");
                return;
            }

            bool requireUppercase = chkUppercase.IsChecked == true;
            bool requireDigits = chkDigits.IsChecked == true;

            User newUser = new User
            {
                Username = username,
                Password = password,
                IsBlocked = false,
                Role = 0,
                MinPasswordLength = GetMinPasswordLength(),
                RequireUppercase = requireUppercase,
                RequireDigits = requireDigits,
            };

            UserManager.AddUser(newUser);
            MessageBox.Show("Пользователь успешно добавлен.");
        }


        private bool IsPasswordValid(string password)
        {
           
            bool requireUppercase = chkUppercase.IsChecked == true;
            bool requireDigits = chkDigits.IsChecked == true;

            bool lengthValid = password.Length >= GetMinPasswordLength();
            bool uppercaseValid = !requireUppercase || Regex.IsMatch(password, @"^(?=.*[A-Z]).+$");
            bool digitsValid = !requireDigits || Regex.IsMatch(password, @"^(?=.*\d).+$");

            return lengthValid && uppercaseValid && digitsValid;
        }
    }
}
