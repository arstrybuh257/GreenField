using System;

namespace GreenField.Api.Models.DeviceReposts
{
    public class PestReport
    {
        public Guid FieldId { get; set; }
        public Guid PestGuid { get; set; }
        public string Concentration { get; set; }
    }
}