using Core.Enums;

namespace Core.Entities
{
    public  class CommonShipping(int id, int tracking, float weight, User employee, User client, ShippingState state,Agency pickup
        ) : Shipping(id, tracking, weight, employee, client, state)
    {
        public Agency PickUpAgency { get; set; } = pickup;
    }
}
