using System;
using System.Collections.Generic;
using GreenField.DAL.ValueObjects;

namespace GreenField.BLL.Types
{
    public class CropDto
    {
        public string Id { get; set; }
        public Guid CultureId { get; set; }
        public string CultureName { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public List<PesticideWithDose> PesticideUsed { get; set; }
        public Guid FieldId { get; set; }
        public Guid OrganisationId { get; set; }
    }
}