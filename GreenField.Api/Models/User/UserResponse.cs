using System;

namespace GreenField.Api.Models.User
{
    public class UserResponse
    {
        public Guid Id { get; set; }
        
        public string Email { get; set; }
        
        public string FirstName { get; set; }
        
        public string LastName { get; set; }
        
        public string Role { get; set; }

        public Guid OrganisationId { get; set; }
    }
}