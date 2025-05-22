namespace Application.UseCases;

using Application.Interfaces;
using Core.Entities;
using Core.Interfaces;
using DTO.Users;

public class UserGetAllClients : IUserGetAllClients
{
	private readonly IUserRepository _userRepository;

	public UserGetAllClients(IUserRepository userRepository)
	{
		_userRepository = userRepository;
	}

	public IEnumerable<UserListDto> Execute()
	{
		IEnumerable<User> users = _userRepository.GetAllClient();
		IEnumerable<UserListDto> userDtos = users.Select(x => new UserListDto(x)).ToList();
		return userDtos;
	}
}