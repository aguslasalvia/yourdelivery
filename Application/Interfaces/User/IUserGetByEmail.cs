using DTO.Users;
namespace Application.Interfaces;

public interface IUserGetByEmail
{
    UserDto Execute(string email);
}