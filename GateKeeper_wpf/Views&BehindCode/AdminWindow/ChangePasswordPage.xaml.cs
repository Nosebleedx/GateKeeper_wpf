using GateKeeper_wpf.Infrasctructure;
using GateKeeper_wpf.Models;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;

namespace GateKeeper_wpf.Views_BehindCode.AdminWindow
{
    public partial class ChangePasswordPage : Page
    {
        private User _currentUser;

        public ChangePasswordPage(User currentUser)
        {
            InitializeComponent();
            _currentUser = currentUser;
            txtUsername.Text = _currentUser.Username;
        }

        private void txtNewPassword_PasswordChanged(object sender, RoutedEventArgs e)
        {
            var password = txtNewPassword.Password;

            // Обновляем требования в зависимости от текущего пользователя
            lblLength.Visibility = Visibility.Visible;
            lblLength.Foreground = password.Length >= _currentUser.MinPasswordLength ? System.Windows.Media.Brushes.Green : System.Windows.Media.Brushes.Red;

            if (_currentUser.RequireUppercase)
            {
                lblUppercase.Visibility = Visibility.Visible;
                lblUppercase.Foreground = Regex.IsMatch(password, @"^(?=.*[A-Z]).+$") ? System.Windows.Media.Brushes.Green : System.Windows.Media.Brushes.Red;
            }
            else
            {
                lblUppercase.Visibility = Visibility.Collapsed;
            }

            if (_currentUser.RequireDigits)
            {
                lblDigits.Visibility = Visibility.Visible;
                lblDigits.Foreground = Regex.IsMatch(password, @"^(?=.*\d).+$") ? System.Windows.Media.Brushes.Green : System.Windows.Media.Brushes.Red;
            }
            else
            {
                lblDigits.Visibility = Visibility.Collapsed;
            }

            lblRepeats.Visibility = Visibility.Visible;
            lblRepeats.Foreground = password.Distinct().Count() == password.Length ? System.Windows.Media.Brushes.Green : System.Windows.Media.Brushes.Red;
        }

        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            string oldPassword = txtOldPassword.Password;
            string newPassword = txtNewPassword.Password;
            string confirmPassword = txtConfirmPassword.Password;

            // Проверка старого пароля
            if (_currentUser.Password != oldPassword)
            {
                MessageBox.Show("Неправильный старый пароль.");
                return;
            }

            // Проверка нового пароля
            if (!IsPasswordValid(newPassword))
            {
                MessageBox.Show("Новый пароль не соответствует требованиям.");
                return;
            }

            // Проверка совпадения паролей
            if (newPassword != confirmPassword)
            {
                MessageBox.Show("Пароли не совпадают.");
                return;
            }

            // Обновление пароля
            _currentUser.Password = newPassword;
            UserManager.SaveUsers();
            MessageBox.Show("Пароль успешно изменен.");
        }

        private bool IsPasswordValid(string password)
        {
            return password.Length >= _currentUser.MinPasswordLength
                   && (!_currentUser.RequireUppercase || Regex.IsMatch(password, @"^(?=.*[A-Z]).+$"))
                   && (!_currentUser.RequireDigits || Regex.IsMatch(password, @"^(?=.*\d).+$"))
                   && password.Distinct().Count() == password.Length;
        }
    }
}
