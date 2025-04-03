using DTO.Users;
namespace Application.Interfaces;

public interface IUserLoginCase
{

    UserDto Execute(UserLoginDto userLogin);

}