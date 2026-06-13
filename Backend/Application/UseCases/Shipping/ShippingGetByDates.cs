using Application.Interfaces;
using Core;
using Core.Enums;
using Core.Interfaces;
using DTO;

namespace Application.UseCases.Shipping;

public class ShippingGetByDates : IShippingGetByDates
{
    private readonly IShippingRepository _shippingRepository;

    public ShippingGetByDates(IShippingRepository shippingRepository)
    {
        _shippingRepository = shippingRepository;
    }

    public List<ShippingDto> Execute(DateTime from, DateTime to, ShippingState state, int userId)
    {
        var shippings = _shippingRepository.GetByDates(from, to, state, userId);
        var shippingDtos = shippings.Select(shipping => new ShippingDto
        {
            Id = shipping.Id,
            Employee = new(shipping.Employee),
            Weight = shipping.Weight,
            State = shipping.State,
            Client = new(shipping.Client),
            CreatedOn = shipping.CreatedOn
        }).ToList();

        return shippingDtos;
    }
}