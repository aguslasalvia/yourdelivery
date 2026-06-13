using Core.Entities;
using Core.Enums;


namespace DTO;

public class ShippingDto
{
	public int Id { get; set; }
	public float Weight { get; set; }
	public UserDto Employee { get; set; }
	public UserDto Client { get; set; }
	public ShippingState State { get; set; }
	public DateTime CreatedOn { get; set; }

	public ShippingDto() { }

	public ShippingDto(Shipping shipping)
	{
		Id = shipping.Id;
		Weight = shipping.Weight;
		Employee = new UserDto(shipping.Employee);
		Client = new UserDto(shipping.Client);
		State = shipping.State;
		CreatedOn = shipping.CreatedOn;
	}
}