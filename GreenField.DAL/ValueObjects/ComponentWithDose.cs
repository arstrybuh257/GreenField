using System;

namespace GreenField.DAL.ValueObjects
{
    public class ComponentWithDose
    {
        public Guid ComponentId { get; set; }
        public double Dose { get; set; }
    }
}