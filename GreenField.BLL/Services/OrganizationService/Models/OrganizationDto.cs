using System;
using GreenField.Common;
using GreenField.Common.Enums;

namespace GreenField.BLL.Services.OrganizationService.Models
{
    public class OrganizationDto
    {
        public Guid Id { get; set; }
        
        public string Name { get; set; }
        
        public Country Country { get; set; }
        
        public string Address { get; set; }
        
        public OrganisationStatus Status { get; set; }
    }
}