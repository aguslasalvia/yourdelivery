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
    
    
    public void Execute(UserDto userDto)
    {
        if (userDto == null || string.IsNullOrEmpty(userDto.Email))
            throw new ArgumentException("Invalid user data");
        
        User existingUser = _userRepository.GetByEmail(userDto.Email);
        if (existingUser == null)
            throw new Exception("User not found");
        
        // Only update if the new value is not null
        if (!string.IsNullOrWhiteSpace(userDto.Name))
            existingUser.Name = userDto.Name;
        if (!string.IsNullOrWhiteSpace(userDto.Lastname))
            existingUser.Lastname = userDto.Lastname;
        if (!string.IsNullOrWhiteSpace(userDto.Phone))
            existingUser.Phone = userDto.Phone;
        if (userDto.Birth != null)
            existingUser.Birth = userDto.Birth;
        if (userDto.Gender != null)
            existingUser.Gender = userDto.Gender;

        _userRepository.Update(existingUser);
    }
}