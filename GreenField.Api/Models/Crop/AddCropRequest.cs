using System;

namespace GreenField.Api.Models.Crop
{
    public class AddCropRequest
    {
        public Guid CultureId { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public int SuccessPercentage { get; set; }
        public Guid FieldId { get; set; }
    }
}