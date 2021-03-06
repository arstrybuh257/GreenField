using System;
using System.Collections.Generic;
using GreenField.Common.Enums;

namespace GreenField.Api.Models.Pesticide
{
    public class CreatePesticideRequest
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public double AveragePrice { get; set; }
        public PesticideType PesticideType { get; set; }
        public List<Guid> ComponentsIds{ get; set; }
    }
}