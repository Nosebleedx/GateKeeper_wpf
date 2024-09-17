using GateKeeper_wpf.Infrasctructure;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace GateKeeper_wpf.Views_BehindCode.AdminWindow
{
    public partial class UserManagementPage : Page
    {
        public UserManagementPage()
        {
            InitializeComponent();
            LoadUsers();
        }

        private void LoadUsers()
        {
            lstUsers.ItemsSource = UserManager.Users.Select(u => u.Username).ToList();
        }


        private void lstUsers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lstUsers.SelectedItem != null)
            {
                var selectedUser = UserManager.Users.FirstOrDefault(u => u.Username == lstUsers.SelectedItem.ToString());

                if (selectedUser != null)
                {
                    txtUsername.Text = selectedUser.Username;
                    txtMinPwdLenght.Text = selectedUser.MinPasswordLength.ToString();
                    txtStatus.Text = selectedUser.IsBlocked ? "Заблокирован" : "Активен";

                    btnBlock.IsEnabled = !selectedUser.IsBlocked;
                    btnUnblock.IsEnabled = selectedUser.IsBlocked;
                }
            }
        }


        private void btnBlock_Click(object sender, RoutedEventArgs e)
        {
            
            var selectedUser = UserManager.Users.FirstOrDefault(u => u.Username == lstUsers.SelectedItem.ToString());
            if (selectedUser != null && selectedUser.Role != 1)
            {
                UserManager.BlockUser(selectedUser.Username);

                UpdateUserInfo();
                MessageBox.Show("Пользователь заблокирован.");
            }
            else
            {
                MessageBox.Show($"Невозможно заблокировать {selectedUser.Username}. \n" +
                    $"Недостаточно привелегий/пользователя не существует в системе.");
            }
        }

        private void btnUnblock_Click(object sender, RoutedEventArgs e)
        {
            var selectedUser = UserManager.Users.FirstOrDefault(u => u.Username == lstUsers.SelectedItem.ToString());
            if (selectedUser != null)
            {
                UserManager.UnblockUser(selectedUser.Username);
                UpdateUserInfo();
                MessageBox.Show("Пользователь разблокирован.");
            }
        }

        private void btnSavePasswordRestrictions_Click(object sender, RoutedEventArgs e)
        {
            var selectedUser = UserManager.Users.FirstOrDefault(u => u.Username == lstUsers.SelectedItem.ToString());
            try
            {
                if (selectedUser != null)
                {
                    selectedUser.MinPasswordLength = int.Parse(txtMinPasswordLength.Text);
                    UserManager.UpdateUser(selectedUser.Username, int.Parse(txtMinPasswordLength.Text));
                    UpdateUserInfo();
                    UserManager.SaveUsers();
                    MessageBox.Show("Ограничения пароля сохранены.");
                }
                else
                {
                    MessageBox.Show("Ошибка выбранный пользователь не найден");
                }
            }
            catch {  }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            var selectedUser = UserManager.Users.FirstOrDefault(u => u.Username == lstUsers.SelectedItem.ToString());
            if (selectedUser != null && selectedUser.Role != 1)
            {

                UserManager.DeleteUser(selectedUser.Username);

                LoadUsers();
                MessageBox.Show("Пользователь удален.");
            }
            else
            {
                MessageBox.Show("Недостаточно привелегий.");
            }
        }

        private void UpdateUserInfo()
        {
            lstUsers_SelectionChanged(null, null);
        }
    }
}
