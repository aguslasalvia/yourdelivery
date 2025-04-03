using Application.Interfaces;
using Core.Interfaces;
using Core.Entities;
using DTO.Users;

namespace Application.UseCases;

public class UserGetByEmail:IUserGetByEmail
{
    private readonly IUserRepository _userRepository;

    public UserGetByEmail(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public UserDto Execute(string email)
    {
        User user = _userRepository.GetByEmail(email);
        UserDto userDto = new UserDto(user);
        return userDto;
    }
}