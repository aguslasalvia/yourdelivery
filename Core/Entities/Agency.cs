namespace Core.Entities
{
    public class Agency
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public float Latitude { get; set; } 
        public float Longitude { get; set; }

        protected Agency(string name, string address, float latitude, float longitude)
        {
            Name = name;
            Address = address;
            Latitude = latitude;
            Longitude = longitude;
            
        }
    }
}
