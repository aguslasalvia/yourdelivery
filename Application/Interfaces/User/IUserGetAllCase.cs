using Core.Entities;
using DTO.Users;

namespace Application.Interfaces;

public interface IUserGetAllCase
{
    IEnumerable<UserListDto> Execute();
}