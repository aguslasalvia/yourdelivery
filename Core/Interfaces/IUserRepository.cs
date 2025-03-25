using Core.Entities;

namespace Core.Interfaces
{
    public interface IUserRepository
    {
        User Add ( User User );
        User Update ( User User );
        User Delete ( User User );
        User? GetByEmail ( String email );

        IEnumerable<User> GetAll ();
    }
}
