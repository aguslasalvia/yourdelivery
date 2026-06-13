using DTO;

namespace Application.Interfaces;

public interface IShippingGetById
{
    ShippingDto Execute(int shippingId);
}