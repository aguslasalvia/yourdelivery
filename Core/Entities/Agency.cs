namespace Core.Entities
{
    
    public class Agency(String name, String direction,float latitude, float longitude)
    {
        public String Name { get; set; } = name;
        public String Direction { get; set; } = direction;
        public float Latitude { get; set; } = latitude;
        public float Longitude { get; set; } = longitude;

        
    }

}
