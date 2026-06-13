using Core.Entities;
using Core.Enums;

namespace  Core.Interfaces
{
    public interface IShippingRepository
    {
        Shipping Add ( Shipping shipping );
        Shipping Update ( Shipping shipping );
        Shipping Delete ( Shipping shipping );
        Shipping GetById ( int id );
        IEnumerable<Shipping> GetByDates(DateTime from, DateTime to, ShippingState state, int userId);
        IEnumerable<Shipping> GetByClientId ( int client );
        IEnumerable<Shipping> GetByCommentary ( string word, int userId );
        IEnumerable<Shipping> GetAllOnProcess();
    }
}
