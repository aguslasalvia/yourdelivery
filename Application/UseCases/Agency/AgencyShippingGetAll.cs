using Application.Interfaces.Agency;
using Core.Interfaces;
using DTO;

namespace Application.UseCases.Agency;
public class AgencyShippingGetAll : IAgencyShippingGetAll
{
	private readonly IAgencyRepository _agencyRepository;

	public AgencyShippingGetAll(IAgencyRepository agencyRepository)
	{
		_agencyRepository = agencyRepository;
	}

	public IEnumerable<AgencyShippingDto> Execute()
	{
		var agencies = _agencyRepository.GetAll();
		return agencies.Select(a => new AgencyShippingDto(a));
	}
}