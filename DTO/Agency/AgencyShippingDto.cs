using Core.Entities;

namespace DTO;

public class AgencyShippingDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Address { get; set; }

    public AgencyShippingDto() { }

    public AgencyShippingDto(Agency agency)
    {
        this.Id = agency.Id;
        this.Name = agency.Name;
        this.Address = agency.Address;
    }
}