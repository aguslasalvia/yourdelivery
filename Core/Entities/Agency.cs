namespace Core.Entities
{
    public class Agency
    {
        public int Id { get; private set; } 

        public string Name { get; private set; }
        public string Address { get; private set; }
        public float Latitude { get; private set; }
        public float Longitude { get; private set; }

        protected Agency() { }

        public Agency(string name, string address, float latitude, float longitude)
        {
            Name = name;
            Address = address;
            Latitude = latitude;
            Longitude = longitude;
        }
    }
}