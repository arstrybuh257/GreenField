using System;

namespace GreenField.Api.Models.User
{
    public class UpdateUserRequest
    {
        public string Email { get; set; }
        
        public string FirstName { get; set; }
        
        public string LastName { get; set; }
        
        public string Role { get; set; }

        public string Password { get; set; }

        public Guid OrganisationId { get; set; }
    }
}