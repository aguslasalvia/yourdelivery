using DTO.Users;
namespace Application.Interfaces;

public interface IUserGetByEmail
{
    UserProfileDto Execute(string email);
}