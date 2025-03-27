using Core.Entities;


namespace Core.Interfaces
{
    public interface IAgencyRepository
    {
        Agency Add ( Agency agency );
        Agency Update ( Agency agency );
        Agency Delete ( Agency agency );

        Agency? GetByName( string name );

        IEnumerable<Agency> GetAll ();
    }
}
