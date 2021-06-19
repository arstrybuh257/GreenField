using System;

namespace GreenField.Api.Models.User
{
    public class CreateOrganisationUserRequest : CreateUserRequest
    {
        public Guid OrganisationId { get; set; }
    }
}