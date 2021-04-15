using System;
using System.Collections.Generic;

namespace GreenField.BLL.Dto
{
    public class WeedDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public List<Guid> Pesticides { get; set; }
    }
}