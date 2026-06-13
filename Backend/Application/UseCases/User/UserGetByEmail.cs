using Application.Interfaces;
using Core.Interfaces;
using Core.Entities;
using DTO;
namespace Application.UseCases;

public class UserGetByEmail:IUserGetByEmail
{
    private readonly IUserRepository _userRepository;

    public UserGetByEmail(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public UserProfileDto Execute(string email)
    {
        User user = _userRepository.GetByEmail(email);
        UserProfileDto userDto = new UserProfileDto(user);
        return userDto;
    }
}