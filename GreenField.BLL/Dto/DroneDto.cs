using System;
using GreenField.Common;

namespace GreenField.BLL.Dto
{
    public class DroneDto
    {
        public Guid Id { get; set; }
        public string Manufacturer { get; set; }
        public DeviceStatus Status { get; set; }
        public byte Battery { get; set; }
    }
}