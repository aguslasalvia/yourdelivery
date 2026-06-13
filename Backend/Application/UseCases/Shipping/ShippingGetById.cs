namespace Application.UseCases.Shipping;

using Application.Interfaces;
using Core.Interfaces;
using Core.Entities;
using DTO;
public class ShippingGetById : IShippingGetById
{

	private readonly IShippingRepository _shippingRepository;
	public ShippingGetById(IShippingRepository shippingRepository)
	{
		_shippingRepository = shippingRepository;
	}

	public ShippingDto Execute(int shippingId)
	{
		Shipping shipping = _shippingRepository.GetById(shippingId);
		
		if (shipping == null)
			return null;
			
		ShippingDto shippingDto = new ShippingDto()
		{
			Id = shipping.Id,
			Employee = new(shipping.Employee),
			Client = new(shipping.Client),
			Weight = shipping.Weight,
			State = shipping.State
			
		};

		return shippingDto;
	}

}