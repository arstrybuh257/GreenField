using GreenField.Common.Enums;

namespace GreenField.DAL.Entities
{
    public class Organisation : BaseEntity
    {
        public string Name { get; set; }
        
        public Country Country { get; set; }
        
        
        public string Address { get; set; }

        public OrganisationStatus Status { get; set; }
    }
}