using System;
using System.Collections.Generic;

namespace GreenField.BLL.Dto
{
    public class FieldDto
    {
        public Guid Id { get; set; }
        public string Address { get; set; }
        public double Area { get; set; }
        public List<CropDto> Crops { get; set; }
        public List<Guid> Sensors { get; set; }
    }
}