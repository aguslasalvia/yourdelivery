using Application.Interfaces;
using Core.Interfaces;
using Core.Entities;
using Core.Enums;
using DTO.Users;

namespace Application.UseCases;

public class UserCreate:IUserCreate
{
    private readonly IUserRepository _userRepository;

    public UserCreate(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    
    
    
    public void Execute(UserRegistrationDto userProfile)
    {
        _userRepository.Add(userProfile.toUser());
    }
}