using System;

namespace GreenField.Api.Models.Field
{
    public class KillWeedRequest
    {
        public Guid FieldId { get; set; }
        public Guid WeedId { get; set; }
        public double Percents { get; set; }
    }
}