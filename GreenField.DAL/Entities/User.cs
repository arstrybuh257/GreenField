using System;
using GreenField.Common.Enums;

namespace GreenField.DAL.Entities
{
    public class User : BaseEntity
    {
        public string FirstName { get; set; }
        
        public string LastName { get; set; }
        
        public string Email { get; set; }

        public Role Role { get; set; }

        public string PasswordHash { get; set; }

        public UserStatus Status { get; set; }

        public Guid? OrganisationId { get; set; }
        public string OrganisationName { get; set; }
    }
}