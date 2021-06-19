using System;

namespace GreenField.Api.Models.User
{
    public abstract class CreateUserRequest
    {
        public string Email { get; set; }
        
        public string FirstName { get; set; }
        
        public string LastName { get; set; }
    }
}