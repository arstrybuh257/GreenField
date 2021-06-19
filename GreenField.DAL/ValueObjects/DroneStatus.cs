using GreenField.Common;

namespace GreenField.DAL.ValueObjects
{
    public class DroneStatus
    {
        public DeviceStatus Status { get; set; }
        
        public byte Battery { get; set; }
        
        public Coordinates Coordinates { get; set; }

        private DroneStatus(DeviceStatus status, byte battery, Coordinates coordinates)
        {
            Status = status;
            Battery = battery;
            Coordinates = coordinates;
        }

        public static DroneStatus GetDefaultStatus()
        {
            return new DroneStatus(DeviceStatus.Disabled, 0, null);
        }
    }
}