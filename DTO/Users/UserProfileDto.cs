using Core.Enums;
using Core.Entities;
namespace DTO.Users;

public class UserProfileDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Lastname { get; set; }
    public string Phone { get; set; }
    public string Email { get; set; }
    public DateOnly Birth { get; set; }
    public string Password { get; set; }
    public Role Role { get; set; }
    public Gender Gender { get; set; }
    public UserStates State { get; set; }
    public int CreatedByID { get; set; }
    public int UpdatedByID { get; set; }
    public DateTime LastUpdated { get; set; }

    public UserProfileDto() { }
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
        State = user.State;
        CreatedByID = user.CreatedByID;
        UpdatedByID = user.UpdatedByID;
        LastUpdated = user.LastUpdated;
    }

    public User toUser()
    {
        User user = new User()
        {
            Id = this.Id,
            Name = this.Name,
            Lastname = this.Lastname,
            Phone = this.Phone,
            Email = this.Email,
            Birth = this.Birth,
            Password = this.Password,
            Role = this.Role,
            Gender = this.Gender,
            State = this.State,
            CreatedByID = this.CreatedByID,
            UpdatedByID = this.UpdatedByID,
            LastUpdated = this.LastUpdated
        };
        return user;
    }
}