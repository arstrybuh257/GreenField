using System;
using System.Collections.Generic;
using GreenField.DAL.ValueObjects;

namespace GreenField.DAL.Entities
{
    public class Pest : BaseEntity
    {
        public string Kind { get; set; }

        public string Description { get; set; }
        
        public List<PesticideWithDose> Pesticides { get; set; }
        
        public List<Guid> Cultures { get; set; }
    }
}