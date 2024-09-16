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
            LoadUsers(); // Загружаем пользователей при загрузке страницы
            
        }

        // Загрузка списка пользователей в ListBox
        private void LoadUsers()
        {
            lstUsers.ItemsSource = UserManager.Users.Select(u => u.Username).ToList();
        }

        // Обработчик изменения выбора в списке пользователей
        private void lstUsers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lstUsers.SelectedItem != null)
            {
                var selectedUser = UserManager.Users.FirstOrDefault(u => u.Username == lstUsers.SelectedItem.ToString());

                if (selectedUser != null)
                {
                    // Обновляем отображаемую информацию о пользователе
                    txtUsername.Text = selectedUser.Username;
                    txtMinPwdLenght.Text = selectedUser.MinPasswordLength.ToString();
                    txtStatus.Text = selectedUser.IsBlocked ? "Заблокирован" : "Активен";

                    // Включаем или отключаем кнопки в зависимости от статуса пользователя
                    btnBlock.IsEnabled = !selectedUser.IsBlocked;
                    btnUnblock.IsEnabled = selectedUser.IsBlocked;
                }
            }
        }

        // Блокировка пользователя
        private void btnBlock_Click(object sender, RoutedEventArgs e)
        {
            
            var selectedUser = UserManager.Users.FirstOrDefault(u => u.Username == lstUsers.SelectedItem.ToString());
            if (selectedUser != null && selectedUser.Role != 1)
            {
                UserManager.BlockUser(selectedUser.Username);
                // Обновляем интерфейс
                UpdateUserInfo();
                MessageBox.Show("Пользователь заблокирован.");
            }
            else
            {
                MessageBox.Show($"Невозможно заблокировать {selectedUser.Username}. \n" +
                    $"Недостаточно привелегий/пользователя не существует в системе.");
            }
        }

        // Разблокировка пользователя
        private void btnUnblock_Click(object sender, RoutedEventArgs e)
        {
            var selectedUser = UserManager.Users.FirstOrDefault(u => u.Username == lstUsers.SelectedItem.ToString());
            if (selectedUser != null)
            {
                UserManager.UnblockUser(selectedUser.Username);
                // Обновляем интерфейс
                UpdateUserInfo();
                MessageBox.Show("Пользователь разблокирован.");
            }
        }
        // Сохранение ограничений пароля для выбранного пользователя
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

        // Удаление пользователя
        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            var selectedUser = UserManager.Users.FirstOrDefault(u => u.Username == lstUsers.SelectedItem.ToString());
            if (selectedUser != null && selectedUser.Role != 1)
            {
                // Удаляем пользователя из коллекции
                UserManager.DeleteUser(selectedUser.Username);

                // Обновляем список пользователей
                LoadUsers();
                MessageBox.Show("Пользователь удален.");
            }
            else
            {
                MessageBox.Show("Недостаточно привелегий.");
            }
        }

        // Обновление информации о пользователе
        private void UpdateUserInfo()
        {
            // Перезагружаем отображаемую информацию о пользователе
            lstUsers_SelectionChanged(null, null);
        }
    }
}
