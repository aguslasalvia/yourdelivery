using DTO;
namespace Application.Interfaces;

public interface IUserLoginCase
{

    UserDto Execute(UserAuthDto userAuth);

}