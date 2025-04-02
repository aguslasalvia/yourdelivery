using Core.Enums;

namespace DTO.User;

public record UserDTO
{
    public int Id { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string Name { get;  set; }
    public string Lastname { get; set; }
    public string Phone { get; set; }
    public DateOnly Birth { get; set; }
    public Role Role { get; set; }
    
    public Gender Gender { get; set; }
    
    
}