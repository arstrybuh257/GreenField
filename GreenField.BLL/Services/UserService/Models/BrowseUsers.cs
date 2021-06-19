using System;
using GreenField.Common.Enums;

namespace GreenField.BLL.Services.UserService.Models
{
    public class BrowseUsers
    {
        public string SearchString { get; set; }
        public Guid? OrganisationId { get; set; }
        public Role? Role { get; set; }
    }
}