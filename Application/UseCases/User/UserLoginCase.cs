using Application.Interfaces;
using Core.Interfaces;
using Core.Entities;
using DTO.Users;
namespace Application.UseCases;


public class UserLoginCase:IUserLoginCase
{
    private  IUserRepository _userRepository;

    public UserLoginCase(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }


    public UserDto Execute(UserLoginDto userLogin)
    { User user = _userRepository.GetByEmailAndPassword(userLogin.Email, userLogin.Password);  
      UserDto? userDto = new UserDto(user);
      return userDto;
    }
    
}