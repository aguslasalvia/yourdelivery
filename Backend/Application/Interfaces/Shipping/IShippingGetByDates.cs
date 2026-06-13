using Core.Enums;
using DTO;

namespace Application.Interfaces;

public interface IShippingGetByDates
{
    List<ShippingDto> Execute(DateTime from, DateTime to, ShippingState state, int id);
}