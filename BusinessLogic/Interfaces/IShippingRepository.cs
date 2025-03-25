using BusinessLogic.Domain;

namespace BusinessLogic.Interfaces
{
    public interface IShippingRepository
    {
        Shipping Add ( Shipping shipping );
        Shipping Update ( Shipping shipping );
        Shipping Delete ( Shipping shipping );
        Shipping? GetById ( int id );

        IEnumerable<Shipping> GetAll();
    }
}
