using GreenField.Common;

namespace GreenField.DAL.Entities
{
    public class Sensor : BaseEntity
    {
        public DeviceStatus Status { get; set; }
        public SensorType SensorType { get; set; }
    }
}