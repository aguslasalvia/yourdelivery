namespace Application.UseCases.Shipping;

using Application.Interfaces;
using Core.Interfaces;
using DTO;
using Core.Enums;
using Core.Entities;

public class ShippingClose : IShippingClose
{

	private readonly IShippingRepository _shippingRepository;
	public ShippingClose(IShippingRepository shippingRepository)
	{
		_shippingRepository = shippingRepository;
	}

	public void Execute(int id)
	{
		var shipping = _shippingRepository.GetById(id);
		shipping.State = ShippingState.Finished;

		_shippingRepository.Update(shipping);
	}

}