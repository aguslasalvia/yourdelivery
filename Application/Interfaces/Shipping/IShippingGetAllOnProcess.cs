using Core.Entities;
using DTO;

namespace Application.Interfaces;

public interface IShippingGetAllOnProcess
{
    IEnumerable<ShippingDto> Execute();
}