
using GateKeeper_wpf.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace GateKeeper_wpf.Infrasctructure
{
    public static class UserManager
    {
        private static string jsonFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "MyLocalDB", "MyLocalDB.json");

        public static List<User> Users { get; private set; }

        static UserManager()
        {
            var directoryPath = Path.GetDirectoryName(jsonFilePath);
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            LoadUsers();
        }

        public static void LoadUsers()
        {
            try
            {
                if (File.Exists(jsonFilePath))
                {
                    string json = File.ReadAllText(jsonFilePath);
                    Users = JsonConvert.DeserializeObject<List<User>>(json) ?? new List<User>();
                }
                else
                {
                    Users = new List<User>
                    {
                        new User
                        {
                            Username = "ADMIN",
                            Password = string.Empty,
                            Role = 1,
                            IsBlocked = false
                        }
                    };

                    SaveUsers();
                    System.Diagnostics.Debug.WriteLine("MyLocalDB.json файл был создан с учетной записью администратора.");
                }
            }
            catch (IOException ex)
            {
                Users = new List<User>();
                System.Diagnostics.Debug.WriteLine("Ошибка при загрузке пользователей: " + ex.Message);
            }
        }

        public static void AddUser(User newUser)
        {
            Users.Add(newUser);
            SaveUsers();
        }

        public static bool ChangePassword(string username, string newPassword)
        {
            var user = Users.FirstOrDefault(u => u.Username == username);
            user.Password = newPassword;
            SaveUsers();
            return true;
        }


        public static void UpdateUser(string username, string newPassword)
        {
            var user = Users.FirstOrDefault(u => u.Username == username);
            if (user != null)
            {
                user.Password = newPassword;
                SaveUsers();  // Сохраняем изменения
            }
        }
        public static void UpdateUser(string username, int minPwdLenght)
        {
            var user = Users.FirstOrDefault(u => u.Username == username);
            if (user != null)
            {
                user.MinPasswordLength = minPwdLenght;
                SaveUsers();  // Сохраняем изменения
            }
        }


        public static void BlockUser(string username)
        {
            var user = Users.FirstOrDefault(u => u.Username == username);
            if (user != null)
            {
                user.IsBlocked = true;
                SaveUsers();  // Сохраняем изменения
            }
        }

        public static void UnblockUser(string username)
        {
            var user = Users.FirstOrDefault(u => u.Username == username);
            if (user != null)
            {
                user.IsBlocked = false;
                SaveUsers();  // Сохраняем изменения
            }
        }

        public static void DeleteUser(string username)
        {
            var user = Users.FirstOrDefault(u => u.Username == username);
            if (user != null)
            {
                Users.Remove(user);
                SaveUsers();  // Сохраняем изменения
            }
        }
        // Сохранение изменений в JSON-файл
        public static void SaveUsers()
        {
            try
            {
                string json = JsonConvert.SerializeObject(Users, Formatting.Indented);
                File.WriteAllText(jsonFilePath, json);
            }
            catch (IOException ex)
            {
                System.Diagnostics.Debug.WriteLine("Error saving users: " + ex.Message);
            }
        }
    }
}
