using System;
using System.Collections.Generic;

namespace GreenField.DAL.Entities
{
    public class Weed : BaseEntity
    {
        public string Name { get; set; }
        public List<Guid> Pesticides { get; set; }
    }
}