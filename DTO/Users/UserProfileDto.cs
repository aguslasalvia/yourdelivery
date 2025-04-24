using Core.Enums;
using Core.Entities;
namespace DTO.Users;

public class UserProfileDto
{
    public int Id { get; set; } 
    public string Name { get;  set; }
    public string Lastname { get;  set; }
    public string Phone { get;  set; }
    public string Email { get;  set; }
    public DateOnly Birth { get;  set; }
    public string Password { get;  set; }
    public Role Role { get;  set; }
    public Gender Gender { get; set; }

    public UserProfileDto(){}
    public UserProfileDto(User user)
    {
        Id = user.Id;
        Name = user.Name;
        Lastname = user.Lastname;
        Phone = user.Phone;
        Email = user.Email;
        Birth = user.Birth;
        Password = user.Password;
        Role = user.Role;
        Gender = user.Gender;
    }

    public User toUser()
    {
        User user = new User()
        {
            Id = Id,
            Name = Name,
            Lastname = Lastname,
            Phone = Phone,
            Email = Email,
            Birth = Birth,
            Password = Password,
            Role = Role,
            Gender = Gender,

        };
        return user;
    }
}