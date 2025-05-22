namespace DTO;

using Core.Entities;

public class ShippingCommentaryDto
{
	public int Id { get; set; }
	public int EmployeeId { get; set; }

	public int ClientID { get; set; }

	public ShippingCommentaryDto(int id, int id1, int id2)
	{
		Id = id;
	}

	public ShippingCommentaryDto(Shipping shipping)
	{
		Id = shipping.Id;
		EmployeeId = shipping.EmployeeID;
		ClientID = shipping.ClientID;
	}

}