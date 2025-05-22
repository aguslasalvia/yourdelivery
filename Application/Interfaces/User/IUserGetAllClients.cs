namespace Application.Interfaces;

using DTO.Users;

public interface IUserGetAllClients
{
		IEnumerable<UserListDto> Execute();
}