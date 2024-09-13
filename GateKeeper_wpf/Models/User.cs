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

        public User()
        {
            
        }
    }

}

