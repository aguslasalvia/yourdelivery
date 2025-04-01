using Core.Enums;

namespace Core.Entities
{
    public class User
    {
        public int Id { get; private set; } 
        public string Name { get; private set; }
        public string Lastname { get; private set; }
        public string Phone { get; private set; }
        public string Email { get; private set; }
        public DateOnly Birth { get; private set; }
        public string Password { get; private set; }
        public Role Role { get; private set; }

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