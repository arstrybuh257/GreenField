using System;
using System.Collections.Generic;
using GreenField.DAL.ValueObjects;

namespace GreenField.Api.Models.Weed
{
    public class UpdateWeedRequest
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public List<PesticideWithDose> Pesticides { get; set; }
    }
}