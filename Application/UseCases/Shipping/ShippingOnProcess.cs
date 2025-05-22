namespace Application.UseCases.Shipping;

using Application.Interfaces;
using Core.Interfaces;
using DTO;
using Core.Enums;
using Core.Entities;

public class ShippingOnProcess : IShippingOnProcess
{

	private readonly IShippingRepository _shippingRepository;
	public ShippingOnProcess(IShippingRepository shippingRepository)
	{
		_shippingRepository = shippingRepository;
	}

	public void Execute(int id)
	{
		var shipping = _shippingRepository.GetById(id);
		shipping.State = ShippingState.OnProcess;

		_shippingRepository.Update(shipping);
	}
}