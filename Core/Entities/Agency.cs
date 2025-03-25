using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    
    public class Agency(String name, String direction,int postal,float latitude, float longitude)
    {
        public String Name { get; set; } = name;
        public String Direction { get; set; } = direction;
        public int Postal { get; set; } = postal;
        public float Latitude { get; set; } = latitude;
        public float Longitude { get; set; } = longitude;

        
    }

}
