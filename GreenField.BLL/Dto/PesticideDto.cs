using System;
using System.Collections.Generic;
using GreenField.Common;

namespace GreenField.BLL.Dto
{
    public class PesticideDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public double AveragePrice { get; set; }
        public List<string> Components { get; set; }
        public PesticideType PesticideType { get; set; }
    }
}