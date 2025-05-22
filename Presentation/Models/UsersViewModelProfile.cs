using Core.Enums;
using DTO.Users;

namespace Presentation.Models;

public class UsersViewModelProfile
{
    public UserProfileDto User { get; set; }
    public Role UserRole { get; set; }
}