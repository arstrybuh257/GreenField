using System;

namespace GreenField.DAL.Entities
{
    public class Crop : BaseEntity
    {
        public Guid CultureId { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public int SuccessPercentage { get; set; }
    }
}