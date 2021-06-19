using Newtonsoft.Json;

namespace GreenField.DAL.ValueObjects
{
    public class FieldCoordinates
    {
        public Coordinates Point1 { get; set; }
        public Coordinates Point2 { get; set; }
        public Coordinates Point3 { get; set; }
        public Coordinates Point4 { get; set; }

        [JsonConstructor]
        public FieldCoordinates(Coordinates p1, Coordinates p2, Coordinates p3, Coordinates p4)
        {
            Point1 = p1;
            Point2 = p2;
            Point3 = p3;
            Point4 = p4;
        }
    }
}