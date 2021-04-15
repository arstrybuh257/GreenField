using System;
using System.Collections.Generic;

namespace GreenField.Api.Models.Pest
{
    public class UpdatePestRequest
    {
        public string Kind { get; set; }
        public List<Guid> Pesticides { get; set; }
    }
}