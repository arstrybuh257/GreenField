using System;
using System.Collections.Generic;
using GreenField.Common.Enums;
using GreenField.DAL.ValueObjects;

namespace GreenField.DAL.Entities
{
    public class Field : BaseEntity
    {
        public string Address { get; set; }
        
        public FieldCoordinates Coordinates { get; set; }
        public double Area { get; set; }
        public List<Crop> Crops { get; set; }
        
        public Crop CurrentCrop { get; set; }
        
        public FieldStatus Status { get; set; }
        public Guid OrganisationId { get; set; }
        
        public string ImagePath { get; set; }
    }
}