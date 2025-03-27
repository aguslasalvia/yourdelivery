using Core.Enums;
namespace DTO.Employee;

public record GetAllEmployeeDTO(String name, String lastname, String phone, DateOnly birth,String email, String password,Role role);