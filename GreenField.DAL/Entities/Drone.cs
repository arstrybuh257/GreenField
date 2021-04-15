using System;
using GreenField.Common;

namespace GreenField.DAL.Entities
{
    public class Drone : BaseEntity
    {
        public string Manufacturer { get; set; }
        public DeviceStatus Status { get; set; }
        public byte Battery { get; set; }
        public Guid OrganisationId { get; set; }
    }
}