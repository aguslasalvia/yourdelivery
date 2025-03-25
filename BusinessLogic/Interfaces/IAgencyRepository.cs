using BusinessLogic.Domain;


namespace BusinessLogic.Interfaces
{
    public interface IAgencyRepository
    {
        Agency Add ( Agency Agency );
        Agency Update ( Agency Agency );
        Agency Delete ( Agency Agency );

        Agency? GetByName( String name );

        IEnumerable<Agency> GetAll ();
    }
}
