using Core.Enums;

namespace Core.Entities
{
	public class CommonShipping : Shipping

	{
		public Agency PickUpAgency { get; set; }

		public int PickupId { get; set; }

		public CommonShipping() { }
		public CommonShipping(int id, float weight, User employee, User client, ShippingState state, Agency pickup) :
				base(id, weight, employee, client, state)
		{

			PickUpAgency = pickup;
			PickupId = pickup.Id;
		}
	}
}
