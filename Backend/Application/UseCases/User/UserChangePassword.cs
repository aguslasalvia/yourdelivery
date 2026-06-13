using DTO;
using Application.Interfaces;
using Core.Interfaces;
using Core.Entities;

namespace Application.UseCases;

public class UserChangePassword : IUserChangePassword
{
    private readonly IUserRepository _userRepository;

    public UserChangePassword(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public void Execute(string email, UserPasswordChangeDto passwordChangeDto)
    {
        if (string.IsNullOrEmpty(email))
            throw new ArgumentException("Email is required");

        if (passwordChangeDto == null)
            throw new ArgumentException("Password change data is required");

        if (string.IsNullOrEmpty(passwordChangeDto.CurrentPassword))
            throw new ArgumentException("Current password is required");

        if (string.IsNullOrEmpty(passwordChangeDto.NewPassword))
            throw new ArgumentException("New password is required");

        User user = _userRepository.GetByEmail(email);

        if (user.Password != passwordChangeDto.CurrentPassword)
            throw new UnauthorizedAccessException("Current password is incorrect");

        if (passwordChangeDto.CurrentPassword == passwordChangeDto.NewPassword)
            throw new ArgumentException("New password must be different from the current password");

        user.Password = passwordChangeDto.NewPassword;
        user.UpdatedByID = user.Id;
        user.LastUpdated = DateTime.Now;

        _userRepository.Update(user);
    }
} 