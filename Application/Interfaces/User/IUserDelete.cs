using DTO.Users;

namespace Application.Interfaces;

public interface IUserDelete
{
    void Execute(UserProfileDto dto);
}