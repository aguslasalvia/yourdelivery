namespace DTO;

using Core.Entities;


public class AgencyDto
{
	public int Id { get; private set; }

	public string Name { get; private set; }
	public string Address { get; private set; }
	public float Latitude { get; private set; }
	public float Longitude { get; private set; }

	protected AgencyDto() { }

	public AgencyDto(Agency agency)
	{
		Id = agency.Id;
		Name = agency.Name;
		Address = agency.Address;
		Latitude = agency.Latitude;
		Longitude = agency.Longitude;
	}

	public Agency toAgency()
	{
		Agency agency = new Agency(){
			Id = this.Id,
			Name = this.Name,
			Address = this.Address,
			Latitude = this.Latitude,
			Longitude = this.Longitude
		};

		return agency;	
	}

}