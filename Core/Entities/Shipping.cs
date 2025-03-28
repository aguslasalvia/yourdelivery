using Core.Enums;

namespace Core.Entities
{
    public  class Shipping {
        public int ID { get; set; } 
        public int Tracking { get; set; } 
        public float Weight { get; set; }

        public int Employee { get; set; } 
        
        public int Client { get; set; } 

        public Role State { get; set; } 

        
        public Shipping() { }
         
        public Shipping(int id, int tracking, float weight, int employeeId, int clientId, Role state)
        {
            ID = id;
            Tracking = tracking;
            Weight = weight; 
            Employee = employeeId;
            Client = clientId;
            State = state;
        }
        public override string ToString()
        {
            return $"{ID} - {Tracking} - {State}";
        }
    }
}
