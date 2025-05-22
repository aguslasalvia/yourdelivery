namespace Application.UseCases.Shipping;

using Application.Interfaces;
using Core.Interfaces;
using Core.Entities;
using DTO;
public class ShippingGetAllOnProcess : IShippingGetAllOnProcess
{
    private readonly IShippingRepository _shippingRepository;

    public ShippingGetAllOnProcess(IShippingRepository shippingRepository)
    {
        _shippingRepository = shippingRepository;
    }

    public IEnumerable<ShippingDto> Execute()
    {
        IEnumerable<Shipping> shippings = _shippingRepository.GetAllOnProcess();
        IEnumerable<ShippingDto> shippingDtos = shippings.Select(x => new ShippingDto(x));
       
        return shippingDtos;
    }
}