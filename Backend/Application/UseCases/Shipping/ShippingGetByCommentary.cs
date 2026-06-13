namespace Application.UseCases.Shipping;

using Application.Interfaces;
using Core.Interfaces;
using DTO;

public class ShippingGetByCommentary : IShippingGetByCommentary
{
	private readonly IShippingRepository _shippingRepository;
	public ShippingGetByCommentary(IShippingRepository shippingRepository)
	{
		_shippingRepository = shippingRepository;
	}

	public List<ShippingDto> Execute(string word, int userId)
	{
		var shippings = _shippingRepository.GetByCommentary(word, userId);
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