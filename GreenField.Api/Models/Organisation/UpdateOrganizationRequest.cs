using GreenField.Common;
using GreenField.Common.Enums;

namespace GreenField.Api.Models.Organisation
{
    public class UpdateOrganizationRequest
    {
        public string Name { get; set; }
        
        public Country Country { get; set; }
        
        public string Address { get; set; }
    }
}