using Application.Interfaces;
using Core.Interfaces;
using Core.Entities;
using DTO.Users;

namespace Application.UseCases;

public class UserGetAllCase:IUserGetAllCase
{
    private IUserRepository _userRepository;

    public UserGetAllCase(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public IEnumerable<UserListDto> Execute()
    {
        IEnumerable<User> users = _userRepository.GetAll();
        IEnumerable<UserListDto> userDtos = users.Select(x => new UserListDto(x));
       
        return userDtos;
    }
}