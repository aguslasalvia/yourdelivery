using Core.Enums;

namespace Core.Entities
{
    public class UrgentShipping(int id, int tracking, float weight, int employeeId, int clientId, 
        Role state,string address,DateTime send,DateTime? arrival ) : Shipping(id, tracking, weight, employeeId, clientId, state)
    {
        public string Address { get; set; } = address;
        public DateTime Send { get; set; } = send;
        public DateTime? Arrival { get; set; } = arrival;


        // Calculate the shipping 
        public bool EfficientDelivery => Arrival.HasValue && (Arrival.Value - Send).TotalHours < 24;
    }
}
