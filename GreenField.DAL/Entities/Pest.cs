using System;
using System.Collections.Generic;

namespace GreenField.DAL.Entities
{
    public class Pest : BaseEntity
    {
        public string Kind { get; set; }
        public List<Guid> Pesticides { get; set; }
    }
}