namespace Core.Entities
{
    
    public class Agency(String name, String address,float latitude, float longitude)
    {
        public String Name { get; set; } = name;
        public String Direction { get; set; } = address;
        public float Latitude { get; set; } = latitude;
        public float Longitude { get; set; } = longitude;

        
    }

}
