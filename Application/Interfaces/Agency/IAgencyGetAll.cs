using DTO;

public interface IAgencyGetAll
{
	IEnumerable<AgencyDto> Execute();
}