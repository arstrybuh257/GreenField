using System;
using System.Collections.Generic;

namespace GreenField.Api.Models.Weed
{
    public class CreateWeedRequest
    {
        public string Name { get; set; }
        public List<Guid> Pesticides { get; set; }
    }
}