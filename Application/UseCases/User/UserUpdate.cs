using DTO.Users;
using Application.Interfaces;
using Core.Interfaces;
using Core.Entities;

namespace Application.UseCases;

public class UserUpdate:IUserUpdate
{
    private readonly IUserRepository _userRepository;

    public UserUpdate(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    
    
    public void Execute(UserProfileDto userDto)
    {
        
        if (userDto == null || string.IsNullOrEmpty(userDto.Email))
            throw new ArgumentException("Invalid user data");
        
        // if (existingUser == null)
        //     throw new Exception("User not found");
        
        // Only update if the new value is not null
        if (string.IsNullOrWhiteSpace(userDto.Name))
            userDto.Name = userDto.Name;
        if (string.IsNullOrWhiteSpace(userDto.Lastname))
            userDto.Lastname = userDto.Lastname;
        if (string.IsNullOrWhiteSpace(userDto.Phone))
            userDto.Phone = userDto.Phone;
        if (userDto.Birth != null)
            userDto.Birth = userDto.Birth;
        if (userDto.Gender != null)
            userDto.Gender = userDto.Gender;

        _userRepository.Update(userDto.toUser());
    }
}