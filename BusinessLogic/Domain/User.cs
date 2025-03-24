using BusinessLogic.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Domain
{
    public class User ( String name, String lastname, String phone, DateOnly birth,String email, String password, Role role)
    {
        public Role Role { get; set; } = role;
        public String Name { get; set; } = name;
        public String Phone { get; set; } = phone;
        public String Email { get; set; } = email;
        public DateOnly Birth { get; set; } = birth;
        public String Password { get; set; } = password;
        public String Lastname { get; set; } = lastname;

    }
}
