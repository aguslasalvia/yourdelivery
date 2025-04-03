using Core.Entities;
namespace DTO.Users;

public class UserListDto
{
    public string Email { get; set; }
    public string Phone { get; set; }
    public string Name { get; set; }
    public string Lastname { get; set; }


    public UserListDto(){}

    public UserListDto(User user)
    {
        Email = user.Email;
        Phone = user.Phone;
        Name = user.Name;
        Lastname = user.Lastname;
    }
}