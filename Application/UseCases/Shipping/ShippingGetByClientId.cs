namespace Application.UseCases.Shipping;

using Application.Interfaces;
using Core.Interfaces;
using DTO;
using DTO.Users;

public class ShippingGetByClientId : IShippingGetByClientId
{
	private readonly IShippingRepository _shippingRepository;
	public ShippingGetByClientId(IShippingRepository shippingRepository)
	{
		_shippingRepository = shippingRepository;
	}

	public IEnumerable<ShippingDto> Execute(int clientId)
	{
		var shippings = _shippingRepository.GetByClientId(clientId);
		var shippingDtos = shippings.Select(shipping => new ShippingDto
		{
			Id = shipping.Id,
			Employee = new(shipping.Employee),
			Weight = shipping.Weight,
			State = shipping.State,
			Client = new(shipping.Client)
		}).ToList();

		return shippingDtos;
	}

}