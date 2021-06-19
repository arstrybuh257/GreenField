using System;

namespace GreenField.DAL.ValueObjects
{
    public class PesticideWithDose
    {
        public Guid PesticideId { get; set; }
        public double Dose { get; set; }
    }
}