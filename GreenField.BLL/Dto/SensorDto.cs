using System;
using GreenField.Common;

namespace GreenField.BLL.Dto
{
    public class SensorDto
    {
        public Guid Id { get; set; }
        public DeviceStatus Status { get; set; }
        public SensorType SensorType { get; set; }
    }
}