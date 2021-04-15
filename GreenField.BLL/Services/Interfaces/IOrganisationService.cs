using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GreenField.BLL.Dto;

namespace GreenField.BLL.Services.Interfaces
{
    public interface IOrganisationService
    {
        public Task<List<OrganisationDto>> BrowseAsync();
        public Task<OrganisationDto> GetAsync(Guid id);
        public Task CreateAsync(OrganisationDto organisation);
        public Task UpdateAsync(OrganisationDto organisation);
        public Task DeleteAsync(Guid id);
        public Task<bool> ExistsAsync(string name);
        public Task<bool> ExistsAsync(Guid id);
    }
}