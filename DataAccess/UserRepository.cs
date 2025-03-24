
using BusinessLogic.Domain;
using BusinessLogic.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DataAccess
{
    public class UserRepository ( DbContext context ) : IUserRepository
    {

        private DbContext _context = context;

        public User Add ( User user )
        {
            _context.Set<User>().Add(user);
            _context.SaveChanges();
            return user;
        }
        public User Update ( User user )
        {
            _context.Entry(user).State = EntityState.Modified;
            _context.SaveChanges();
            return user;
        }

        public User Delete ( User user )
        {
            _context.Remove(user);
            return user;
        }
        public User? GetByEmail ( string email )
        {
            return _context.Set<User>().FirstOrDefault(user => user.Email == email);

        }

        public IEnumerable<User> GetAll ()
        {
            return _context.Set<User>().ToList();
        }
    }
}
