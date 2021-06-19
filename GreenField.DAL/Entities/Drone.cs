using System;
using GreenField.Common;
using GreenField.DAL.ValueObjects;

namespace GreenField.DAL.Entities
{
    public class Drone : BaseEntity
    {
        public string Manufacturer { get; set; }
        
        public string Model { get; set; }

        public DroneStatus Status { get; set; }

        public Guid OrganisationId { get; set; }
    }
}