using Core.Enums;
using Core.Entities;

namespace DTO.Users;

public class UserDto
{
    public int Id { get; set; }
    public string Email { get; set; }
    public string Name { get; set; }
    public string Lastname { get; set; }
    public string Phone { get; set; }
    public DateOnly Birth { get; set; }
    public Role Role { get; set; }

    public Gender Gender { get; set; }

    public UserDto(){}

public UserDto(User user)
    {
        Id = user.Id;
        Email = user.Email;
        Name = user.Name;
        Lastname = user.Lastname;
        Phone = user.Phone;
        Birth = user.Birth;
        Role = user.Role;
        Gender = user.Gender;
    }

    public User toUser()
    {
        User user = new(){
            Id = this.Id,
            Email = this.Email,
            Name = this.Name,
            Lastname = this.Lastname,
            Phone = this.Phone,
            Birth = this.Birth,
            Role = this.Role,
            Gender = this.Gender,
        };
        
        return user;
    }
    
}