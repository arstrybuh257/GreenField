using System;
using GreenField.Common;
using GreenField.Common.Enums;

namespace GreenField.Api.Models.Sensor
{
    public class CreateSensorRequest
    {
        public SensorType SensorType { get; set; }
        public Guid? FieldId { get; set; }
    }
}