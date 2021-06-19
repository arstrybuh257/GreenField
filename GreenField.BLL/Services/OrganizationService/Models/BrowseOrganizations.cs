using GreenField.Common.Enums;

namespace GreenField.BLL.Services.OrganizationService.Models
{
    public class BrowseOrganizations
    {
        public string Name { get; set; }
        
        public Country? Country { get; set; }
    }
}