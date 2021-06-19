using System;
using GreenField.Common.Enums;

namespace GreenField.BLL.Services.UserService.Models
{
    public class UserDto
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        
        public string FirstName { get; set; }
        
        public string LastName { get; set; }
        
        public Role Role { get; set; }
        
        public UserStatus Status { get; set; }

        public Guid OrganisationId { get; set; }
        public string OrganisationName{ get; set; }
    }
}