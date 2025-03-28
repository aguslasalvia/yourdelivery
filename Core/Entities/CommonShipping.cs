using Core.Enums;

namespace Core.Entities
{
    public  class CommonShipping(int id, int tracking, float weight, int employeeId, int clientId, Role state,Agency pickup
        ) : Shipping(id, tracking, weight, employeeId, clientId, state)
    {
        public Agency PickUpAgency { get; set; } = pickup;
    }
}
