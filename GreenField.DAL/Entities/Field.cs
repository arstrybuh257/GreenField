using System;
using System.Collections.Generic;

namespace GreenField.DAL.Entities
{
    public class Field : BaseEntity
    {
        public string Address { get; set; }
        public double Area { get; set; }
        public List<Crop> Crops { get; set; }
        public List<Guid> Sensors { get; set; }
        public Guid OrganisationId { get; set; }
    }
}