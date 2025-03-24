using BusinessLogic.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Domain
{
    public class Shipping (int id,int tracking,decimal weight,User employee,User client,ShippingState state)
    {
        public int ID { get; set; } = id;
        public int Tracking { get; set; } = tracking;
        public decimal Weight { get; set; } = weight;
        
        public User Employee { get; set; } = employee;
        public User Client { get; set; } = client;

        public ShippingState State { get; set; } = state;


    }
}
