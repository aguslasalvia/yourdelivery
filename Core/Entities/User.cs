using Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class User ( string name, string lastname, string phone, DateOnly birth,string email, string password, Role role)
    {
        public Role Role { get; set; } = role;
        public string Name { get; set; } = name;
        public string Phone { get; set; } = phone;
        public string Email { get; set; } = email;
        public DateOnly Birth { get; set; } = birth;
        public string Password { get; set; } = password;
        public string Lastname { get; set; } = lastname;

    }
}
