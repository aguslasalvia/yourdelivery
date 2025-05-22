using Core.Interfaces;
using DTO;

namespace Application.UseCases.Agency;
public class AgencyGetAll : IAgencyGetAll
{
	private readonly IAgencyRepository _agencyRepository;

	public AgencyGetAll(IAgencyRepository agencyRepository)
	{
		_agencyRepository = agencyRepository;
	}

	public IEnumerable<AgencyDto> Execute()
	{
		var agencies = _agencyRepository.GetAll();
		return agencies.Select(a => new AgencyDto(a));
	}
}