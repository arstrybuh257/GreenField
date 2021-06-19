using System;
using GreenField.Common;
using GreenField.Common.Enums;

namespace GreenField.BLL.Services.SensorService.Models
{
    public class SensorDto
    {
        public Guid Id { get; set; }
        public DeviceStatus Status { get; set; }
        public SensorType SensorType { get; set; }
        public Guid OrganisationId { get; set; }
        public Guid FieldId { get; set; }
    }
}