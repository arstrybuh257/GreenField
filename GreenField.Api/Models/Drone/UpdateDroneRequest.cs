using GreenField.Common;

namespace GreenField.Api.Models.Drone
{
    public class UpdateDroneRequest
    {
        public string Manufacturer { get; set; }
        public string Model { get; set; }
    }
}