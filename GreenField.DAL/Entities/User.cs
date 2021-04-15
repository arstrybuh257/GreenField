using System;

namespace GreenField.DAL.Entities
{
    public class User : BaseEntity
    {
        public string Email { get; set; }
        
        public string FirstName { get; set; }
        
        public string LastName { get; set; }
        
        public string Role { get; set; }

        public string Password { get; set; }

        public Guid OrganisationId { get; set; }
    }
}