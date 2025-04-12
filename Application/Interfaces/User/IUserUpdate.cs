namespace Application.Interfaces;
using DTO.Users;
public interface IUserUpdate
{
    void Execute(UserDto dto);
}