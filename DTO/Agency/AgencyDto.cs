namespace DTO;

using Core.Entities;


public class AgencyDto
{
	public int Id { get;  set; }

	public string Name { get;  set; }
	public string Address { get;  set; }
	public float Latitude { get;  set; }
	public float Longitude { get;  set; }

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