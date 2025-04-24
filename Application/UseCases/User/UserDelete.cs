using Application.Interfaces;
using Core.Interfaces;
using DTO.Users;
namespace Application.UseCases;

public class UserDelete:IUserDelete
{
    private readonly IUserRepository _userRepository;

    public UserDelete(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public void Execute(UserProfileDto dto)
    {
        _userRepository.Delete(dto.toUser());
    }
}