using Application.Interfaces;
using Core.Interfaces;
using Core.Entities;
using DTO;
namespace Application.UseCases;


public class UserLoginCase:IUserLoginCase
{
    private  IUserRepository _userRepository;

    public UserLoginCase(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public UserDto Execute(UserAuthDto userAuth)
    { 
        User user = _userRepository.GetByEmailAndPassword(userAuth.Email, userAuth.Password);
        UserDto userDto = new UserDto(user);
        return userDto;
    }
    
}