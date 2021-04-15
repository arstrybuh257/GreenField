using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GreenField.DAL.Entities;

namespace GreenField.DAL.Repositories.Interfaces
{
    public interface IPestRepository
    {
        public Task<IEnumerable<Pest>> BrowseAsync();
        public Task<Pest> GetAsync(Guid id);
        public Task CreateAsync(Pest pest);
        public Task UpdateAsync(Pest pest);
        public Task DeleteAsync(Guid id);
        public Task<bool> ExistsAsync(string name);
        public Task<bool> ExistsAsync(Guid id);
    }
}