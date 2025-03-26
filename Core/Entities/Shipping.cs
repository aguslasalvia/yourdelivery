using Core.Enums;

namespace Core.Entities
{
    public  class Shipping (int id,int tracking,decimal weight,User employee,User client,ShippingState state)
    {
        public int ID { get; set; } = id;
        public int Tracking { get; set; } = tracking;
        public decimal Weight { get; set; } = weight;
        
        public User Employee { get; set; } = employee;
        public User Client { get; set; } = client;

        public ShippingState State { get; set; } = state;


        public override string ToString()
        {
            return $"{ID} - {Tracking} - {State}";
        }
    }
}
