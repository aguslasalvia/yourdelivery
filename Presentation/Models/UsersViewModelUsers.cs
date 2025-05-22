using Core.Entities;
using Core.Enums;
using DTO.Users;

namespace Presentation.Models;

public class UsersViewModelUsers
{
    public List<UserListDto> Users { get; set; }
    public string Message { get; set; }
}