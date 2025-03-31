using Core.Entities;
using Core.Interfaces;
using Infrastructure.Persistence;

namespace Infrastructure.Repositories;

public class UserRepository:IUserRepository
{
    private readonly AppDbContext _context;

    public UserRepository(AppDbContext context)
    {
        _context = context;
    }


    public User Add(User user)
    {
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

    public User Delete(User user)
    {
        _context.Users.Remove(user);
        _context.SaveChanges();
        return user;
    }

    public User? GetByEmail(string email)
    {
        email = email.Trim().ToLower();
        var user = _context.Users.FirstOrDefault(u => u.Email == email);
        Console.WriteLine(user);
        return user;
    }

    public User? GetByEmailAndPassword(string username, string password)
    {
        var user = _context.Users.FirstOrDefault(u => u.Email == username && u.Password == password);
        return user;
    }

    public IEnumerable<User> GetAll()
    {
        var users = _context.Users.ToList();
        return users;
    }
}