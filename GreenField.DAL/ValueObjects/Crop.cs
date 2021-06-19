using System;
using System.Collections.Generic;
using GreenField.DAL.Entities;

namespace GreenField.DAL.ValueObjects
{
    public class Crop : BaseEntity
    {
        public Guid CultureId { get; set; }
        
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        
        public List<PesticideWithDose> PesticideUsed { get; set; }
    }
}