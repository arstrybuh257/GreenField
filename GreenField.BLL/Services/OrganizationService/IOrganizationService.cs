using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GreenField.BLL.Dto;
using GreenField.BLL.Services.OrganizationService.Models;

namespace GreenField.BLL.Services.OrganizationService
{
    public interface IOrganizationService
    {
        public Task<List<OrganizationDto>> BrowseAsync(BrowseOrganizations query);
        public Task<OrganizationDto> GetAsync(Guid id);
        public Task CreateAsync(OrganizationDto organization);
        public Task UpdateAsync(OrganizationDto organization);
        public Task DeleteAsync(Guid id);
        public Task<bool> ExistsAsync(string name);
        public Task<bool> ExistsAsync(Guid id);
    }
}