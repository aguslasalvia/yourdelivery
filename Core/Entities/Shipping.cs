using Core.Enums;

namespace Core.Entities
{
    public  class Shipping {
        public int ID { get; set; } 
        public int Tracking { get; set; } 
        public float Weight { get; set; }

        public User Employee { get; set; } 
        
        public User Client { get; set; } 

        public ShippingState State { get; set; } 

        
        public Shipping() { }
         
        public Shipping(int id, int tracking, float weight, User employee, User client, ShippingState state)
        {
            ID = id;
            Tracking = tracking;
            Weight = weight; 
            Employee = employee;
            Client = client;
            State = state;
        }
        public override string ToString()
        {
            return $"{ID} - {Tracking} - {State}";
        }
    }
}
