using Core.Entities;

namespace Core.Interfaces
{
    public interface IShippingRepository
    {
        Shipping Add ( Shipping shipping );
        Shipping Update ( Shipping shipping );
        Shipping Delete ( Shipping shipping );
        Shipping GetById ( int id );

				IEnumerable<Shipping> GetByClientId ( int client );
        IEnumerable<Shipping> GetAllOnProcess();
    }
}
