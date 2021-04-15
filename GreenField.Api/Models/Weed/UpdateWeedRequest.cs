using System;
using System.Collections.Generic;

namespace GreenField.Api.Models.Weed
{
    public class UpdateWeedRequest
    {
        public string Name { get; set; }
        public List<Guid> Pesticides { get; set; }
    }
}