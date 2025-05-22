using Application.Interfaces.Agency;
using Core.Interfaces;
using DTO;

namespace Application.UseCases.Agency;

public class AgencyGetById : IAgencyGetById
{
    private readonly IAgencyRepository _agencyRepository;

    public AgencyGetById(IAgencyRepository agencyRepository)
    {
        _agencyRepository = agencyRepository;
    }
    
    public AgencyDto Execute(int id)
    {
        var agency = _agencyRepository.GetById(id);
        return new AgencyDto(agency);
    }
}