using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GreenField.DAL.Entities;

namespace GreenField.DAL.Repositories.Interfaces
{
    public interface ICultureRepository
    {
        public Task<IEnumerable<Culture>> BrowseAsync();
        public Task<Culture> GetAsync(Guid id);
        public Task CreateAsync(Culture culture);
        public Task UpdateAsync(Culture culture);
        public Task DeleteAsync(Guid id);
        public Task<bool> ExistsAsync(string name);
        public Task<bool> ExistsAsync(Guid id);
    }
}