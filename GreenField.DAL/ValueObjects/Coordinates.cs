using Newtonsoft.Json;

namespace GreenField.DAL.ValueObjects
{
    public class Coordinates
    {
        public double Lat { get; set; }
        public double Lon { get; set; }

        [JsonConstructor]
        public Coordinates(double lat, double lon)
        {
            Lat = lat;
            Lon = lon;
        }
    }
}