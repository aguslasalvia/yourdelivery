using Core.Entities;
using Core.Interfaces;
using Core.Enums;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.X509Certificates;

namespace Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
	private readonly AppDbContext _context;
	public UserRepository(AppDbContext context)
	{
		_context = context;
	}

	public User Add(User user)
	{
		if (user.Password.Length < 6)
		{
			throw new Exception("Password must be at least 6 characters long");
		}

		int numbers = 0;
		int letters = 0;
		int specialChars = 0;

		foreach (char c in user.Password)
		{
			if (char.IsDigit(c))
				numbers++;
			else if (char.IsLetter(c))
				letters++;
			else
				specialChars++;
		}

		if (numbers == 0)
			throw new Exception("Password must contain at least one number");
		if (letters == 0)
			throw new Exception("Password must contain at least one letter");
		if (specialChars == 0)
			throw new Exception("Password must contain at least one special character");

		_context.Users.Add(user);
		_context.SaveChanges();
		return user;
	}

	public User Update(User user)
	{
		_context.Users.Update(user);
		_context.SaveChanges();
		return user;
	}

	public void Delete(User user)
	{
		User u = _context.Users.FirstOrDefault(u => u.Id == user.Id);
		if (u == null)
			throw new Exception("User not found");

		u.State = UserStates.Inactive;
		_context.Users.Update(u);
		_context.SaveChanges();
	}

	public User? GetByEmail(string email)
	{
		email = email.Trim().ToLower();
		var user = _context.Users.FirstOrDefault(u => u.Email == email && u.State == UserStates.Active);
		if (user == null)
			throw new Exception("User not found");

		return user;
	}

	public User? GetByEmailAndPassword(string email, string password)
	{
		var user = _context.Users.FirstOrDefault(u => u.Email == email && u.Password == password && u.State == UserStates.Active);
		return user ?? throw new Exception("User Not Found");
	}

	public IEnumerable<User> GetAll()
	{
		var users = _context.Users.Where(u => u.State == UserStates.Active).ToList();
		return users ?? throw new Exception("No users have been found");
	}

	public IEnumerable<User> GetAllClient()
	{
		var users = _context.Users
			.Where(u => u.Role == Role.Client && u.State == UserStates.Active)
			.ToList();

		return users ?? throw new Exception("No users have been found");
	}
}