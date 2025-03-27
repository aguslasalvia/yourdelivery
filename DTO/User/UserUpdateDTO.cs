using Core.Enums;

namespace DTO.User;

public record UserUpdateDTO(String name, String lastname, String phone, DateOnly birth,String email, String password, Role role);