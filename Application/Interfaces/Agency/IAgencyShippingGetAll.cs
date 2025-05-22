using DTO;

namespace Application.Interfaces.Agency;

public interface IAgencyShippingGetAll
{
    IEnumerable<AgencyShippingDto> Execute();
}