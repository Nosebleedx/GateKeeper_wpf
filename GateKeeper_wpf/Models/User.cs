using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GateKeeper_wpf.Models
{
    public class User
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public int Role { get; set; }  // 0 - обычный пользователь, 1 - админ

        public int MinPasswordLength { get; set; } = 8;  // Минимальная длина по умолчанию

        public bool RequireUppercase { get; set; } = true;  // Требование для верхнего регистра по умолчанию

        public bool RequireDigits { get; set; } = true;  // Требование для цифр по умолчанию

        public bool IsBlocked { get; set; } = false;

        public User()
        {
            
        }
    }

}

