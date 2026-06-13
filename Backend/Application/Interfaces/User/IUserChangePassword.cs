using DTO;
namespace Application.Interfaces;

public interface IUserChangePassword
{
    void Execute(string email, UserPasswordChangeDto passwordChangeDto);
} 