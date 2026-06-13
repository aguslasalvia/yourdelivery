using DTO;

namespace Application.Interfaces;

public interface IShippingGetByClientId
{
    List<ShippingDto> Execute(int clientId);
}