using System;
using System.Collections.Generic;
using GreenField.DAL.ValueObjects;

namespace GreenField.BLL.Services.PestService.Models
{
    public class PestDto
    {
        public Guid Id { get; set; }
        public string Kind { get; set; }
        public string Description { get; set; }
        public List<PesticideWithDose> Pesticides { get; set; }
        public List<Guid> Cultures { get; set; }
    }
}