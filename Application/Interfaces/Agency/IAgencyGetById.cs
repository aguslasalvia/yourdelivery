using DTO;
namespace Application.Interfaces.Agency;

public interface IAgencyGetById
{
    AgencyDto Execute(int id);
}