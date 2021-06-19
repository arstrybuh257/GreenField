using System;
using GreenField.Common;
using GreenField.Common.Enums;

namespace GreenField.DAL.Entities
{
    public class Sensor : BaseEntity
    {
        public DeviceStatus Status { get; set; }
        
        public SensorType SensorType { get; set; }

        public Guid FieldId { get; set; }

        public Guid OrganisationId { get; set; }
    }
}