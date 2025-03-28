using Core.Enums;

namespace DTO.User;

public record UserUpdateDTO(string name, string lastname, string phone, DateOnly birth,string email, string password, Role role);