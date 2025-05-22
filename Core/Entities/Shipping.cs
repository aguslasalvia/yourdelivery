using Core.Enums;

namespace Core.Entities
{
    public  class Shipping
    {
        public int Id { get; set; } 
        public float Weight { get; set; }

        public User Employee { get; set; } 
        public int EmployeeID { get; set; }
        
        public User Client { get; set; } 
				public int ClientID { get; set; }


        public ShippingState State { get; set; } 
        
        public Shipping() { }
         
        public Shipping(int id, float weight, User employee, User client, ShippingState state)
        {
            Id = id;
            Weight = weight; 
            Employee = employee;
            EmployeeID = employee.Id;
            Client = client;
						ClientID = client.Id;
            State = state;
        }
				
        // public override string ToString()
        // {
        //     return $"{Id} - {Tracking} - {State}";
        // }
    }
}
