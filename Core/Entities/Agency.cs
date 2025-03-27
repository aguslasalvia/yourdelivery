namespace Core.Entities
{
    public class Agency(string name, string address,float latitude, float longitude)
    {
        public string Name { get; set; } = name;
        public string Address { get; set; } = address;
        public float Latitude { get; set; } = latitude;
        public float Longitude { get; set; } = longitude;
    }
}
