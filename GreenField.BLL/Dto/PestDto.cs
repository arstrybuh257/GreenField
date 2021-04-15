using System;
using System.Collections.Generic;

namespace GreenField.BLL.Dto
{
    public class PestDto
    {
        public Guid Id { get; set; }
        public string Kind { get; set; }
        public List<Guid> Pesticides { get; set; }
    }
}