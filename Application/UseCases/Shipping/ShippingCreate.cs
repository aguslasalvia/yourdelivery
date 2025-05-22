using Application.Interfaces;
using Core.Interfaces;
using DTO;

namespace Application.UseCases.Shipping;

public class ShippingCreate : IShippingCreate
{
    private readonly IShippingRepository _shippingRepository;

    public ShippingCreate(IShippingRepository shippingRepository)
    {
        _shippingRepository = shippingRepository;
    }

    public void ExecuteCommon(CreateCommonShippingDto createCommonShipping)
    {
        _shippingRepository.Add(createCommonShipping.toCommonShipping());
    }

    public void ExecuteUrgent(CreateUrgentShippingDto createUrgentShipping)
    {
        _shippingRepository.Add(createUrgentShipping.toUrgentShipping());
    }
}