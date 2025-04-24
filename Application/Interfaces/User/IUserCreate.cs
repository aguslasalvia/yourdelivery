using DTO.Users;

namespace Application.Interfaces;

public interface IUserCreate
{
    void Execute(UserRegistrationDto user);
}