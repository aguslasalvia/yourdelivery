using Core.Entities;

namespace Core.Interfaces
{
    public interface IUserRepository
    {
        User Add ( User user );
        User Update ( User user );
        User Delete ( User user );
        User? GetByEmail ( string email );

        User? GetByEmailAndPassword ( string email, string password );
        
        IEnumerable<User> GetAll ();
        
    }
}
