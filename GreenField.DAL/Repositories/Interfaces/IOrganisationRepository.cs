using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GreenField.DAL.Entities;

namespace GreenField.DAL.Repositories.Interfaces
{
    public interface IOrganisationRepository
    {
        public Task<IEnumerable<Organisation>> BrowseAsync();
        public Task<Organisation> GetAsync(Guid id);
        public Task CreateAsync(Organisation organisation);
        public Task UpdateAsync(Organisation organisation);
        public Task DeleteAsync(Guid id);
        public Task<bool> ExistsAsync(string name);
        public Task<bool> ExistsAsync(Guid id);
    }
}