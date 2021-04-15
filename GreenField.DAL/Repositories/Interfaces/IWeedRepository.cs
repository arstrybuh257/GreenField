using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GreenField.DAL.Entities;

namespace GreenField.DAL.Repositories.Interfaces
{
    public interface IWeedRepository
    {
        public Task<IEnumerable<Weed>> BrowseAsync();
        public Task<Weed> GetAsync(Guid id);
        public Task CreateAsync(Weed weed);
        public Task UpdateAsync(Weed weed);
        public Task DeleteAsync(Guid id);
        public Task<bool> ExistsAsync(string name);
        public Task<bool> ExistsAsync(Guid id);
    }
}