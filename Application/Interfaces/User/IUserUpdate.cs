namespace Application.Interfaces;
using DTO.Users;
public interface IUserUpdate
{
    UserDto Execute(UserDto dto);
}