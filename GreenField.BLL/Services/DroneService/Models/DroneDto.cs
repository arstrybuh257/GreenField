using System;
using GreenField.Common;
using GreenField.DAL.ValueObjects;

namespace GreenField.BLL.Services.Drone.Models
{
    public class DroneDto
    {
        public Guid Id { get; set; }
        
        public string Manufacturer { get; set; }
        
        public string Model { get; set; }
        public DroneStatus Status { get; set; }
        
        public Guid OrganisationId { get; set; }
    }
}