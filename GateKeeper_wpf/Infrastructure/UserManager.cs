using GateKeeper_wpf.Infrasctructure.Helpers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using GateKeeper_wpf.Models;

namespace GateKeeper_wpf.Infrasctructure
{
    public static class UserManager
    {
        private static string jsonFilePath = "C:\\Users\\Дмитрий\\source\\repos\\GateKeeper_wpf\\GateKeeper_wpf\\MyLocalDB\\MyLocalDB.json"
            ?? throw new ArgumentNullException("jsonFilePath", "Переменная окружения jsonFilePath не задана.");
        public static List<User> Users { get; private set; }  // Список пользователей

        static UserManager()
        {
            LoadUsers();  // Загрузка списка при старте
        }

        // Загрузка пользователей из JSON-файла
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
                    Users = new List<User>();  // Если файла нет, создаем пустой список
                }
            }
            catch (IOException ex)
            {
                // Обработка ошибок чтения файла
                Users = new List<User>();
                System.Diagnostics.Debug.WriteLine("Error loading users: " + ex.Message);
            }
        }

        // Добавление нового пользователя в список и обновление файла
        public static void AddUser(User newUser)
        {
            Users.Add(newUser);  // Добавляем нового пользователя в список
            SaveUsers();  // Сохраняем изменения в файл
        }

        private static void AddNewUser(string username, string password, int role)
        {
            var hashedPassword = AuthHelper.HashPassword(password);  // Хеширование пароля
            var newUser = new User { Username = username, Password = hashedPassword, Role = role };
            AddUser(newUser);  // Добавляем нового пользователя
            MessageBox.Show("User added successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
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
