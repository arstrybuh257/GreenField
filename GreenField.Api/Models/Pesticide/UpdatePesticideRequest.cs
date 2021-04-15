using System.Collections.Generic;
using GreenField.Common;

namespace GreenField.Api.Models.Pesticide
{
    public class UpdatePesticideRequest
    {
        public string Name { get; set; }
        public double AveragePrice { get; set; }
        public List<string> Components { get; set; }
        public PesticideType PesticideType { get; set; }
    }
}