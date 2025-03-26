using Core.Enums;

namespace Core.Entities
{
    public class UrgentShipping(int id, int tracking, decimal weight, User employee, User client, 
        ShippingState state,String direction,DateTime send,DateTime? arrival ) : Shipping(id, tracking, weight, employee, client, state)
    {
        public String Direction { get; set; } = direction;
        public DateTime Send { get; set; } = send;
        public DateTime? Arrival { get; set; } = arrival;


        // Calculate the shipping 
        public bool EfficientDelivery => Arrival.HasValue && (Arrival.Value - Send).TotalHours < 24;
    }
}
