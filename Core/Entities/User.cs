using Core.Enums;

namespace Core.Entities
{
    public class User
    {
        public int Id { get; private set; } 
        public string Name { get;  set; }
        public string Lastname { get;  set; }
        public string Phone { get;  set; }
        public string Email { get;  set; }
        public DateOnly Birth { get;  set; }
        public string Password { get;  set; }
        public Role Role { get;  set; }

        protected User() { } 

        public User(string name, string lastname, string phone, DateOnly birth, string email, string password, Role role)
        {
            Name = name;
            Lastname = lastname;
            Phone = phone;
            Email = email;
            Birth = birth;
            Password = password;
            Role = role;
        }
    }
}