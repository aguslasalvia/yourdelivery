using DTO;

namespace Application.Interfaces;

public interface IShippingGetByClientId
{
		IEnumerable<ShippingDto> Execute(int clientId);
}