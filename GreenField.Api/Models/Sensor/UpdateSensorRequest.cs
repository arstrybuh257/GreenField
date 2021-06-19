using System;
using GreenField.Common;
using GreenField.Common.Enums;

namespace GreenField.Api.Models.Sensor
{
    public class UpdateSensorRequest
    {
        public SensorType SensorType { get; set; }
        public Guid? FieldId { get; set; }
        public DeviceStatus Status { get; set; }
    }
}