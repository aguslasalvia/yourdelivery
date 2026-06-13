using Core.Enums;

namespace Core.Entities
{
    public class UrgentShipping : Shipping
    {
        public string Address { get; set; }
        public DateTime? Send { get; set; }
        public DateTime? Arrival { get; set; }


        // Calculate the shipping 
        public bool EfficientDelivery => Arrival.HasValue && (Arrival.Value - Send)?.TotalHours < 24;

        public UrgentShipping()
        {
        }

        public UrgentShipping(int id, float weight, User employee, User client,
            ShippingState state, string address, DateTime send, DateTime? arrival) : base(id,
            weight, employee, client, state)
        {
            Arrival = arrival;
            Address = address;
            Send = send;
        }
    }
}