using Core.Entities;
using Core.Interfaces;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

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

    public void Delete(User user)
    {
        User u = _context.Users.FirstOrDefault(u => u.Email == user.Email);
        if (u == null)
            throw new Exception("User not found");
        
        _context.Users.Remove(u);
        _context.SaveChanges();
        // Not returning anything because I think it's better
        // not to return a user we have already deleted :)
    }

    public User? GetByEmail(string email)
    {
        email = email.Trim().ToLower();
        var user = _context.Users.FirstOrDefault(u => u.Email == email);
        if (user == null)
            throw new Exception("User not found");
        
        Console.WriteLine(user);
        return user;
    }

    public User? GetByEmailAndPassword(string email, string password)
    {
        var user = _context.Users.FirstOrDefault(u => u.Email == email && u.Password == password);
        if (user == null)
            throw new Exception("User not found");

        return user;
    }

    public IEnumerable<User> GetAll()
    {
        var users = _context.Users.ToList();
        if (users == null)
            throw new Exception("No users have been found");

        return users;
    }
}