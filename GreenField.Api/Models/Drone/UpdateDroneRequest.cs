using GreenField.Common;

namespace GreenField.Api.Models.Drone
{
    public class UpdateDroneRequest
    {
        public string Manufacturer { get; set; }
        public DeviceStatus Status { get; set; }
        public byte Battery { get; set; }
    }
}