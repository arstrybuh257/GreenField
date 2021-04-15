using System.Collections.Generic;
using GreenField.Common;

namespace GreenField.DAL.Entities
{
    public class Pesticide : BaseEntity
    {
        public string Name { get; set; }
        public double AveragePrice { get; set; }
        public List<string> Components { get; set; }
        public PesticideType PesticideType { get; set; }
    }
}