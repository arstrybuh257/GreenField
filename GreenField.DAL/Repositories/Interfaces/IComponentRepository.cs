using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GreenField.DAL.Entities;

namespace GreenField.DAL.Repositories.Interfaces
{
    public interface IComponentRepository
    {
        public Task<IEnumerable<Component>> BrowseAsync();
        public Task<Component> GetAsync(Guid id);
        public Task CreateAsync(Component component);
        public Task UpdateAsync(Component component);
        public Task DeleteAsync(Guid id);
        public Task<bool> ExistsAsync(string name);
        public Task<bool> ExistsAsync(Guid id);
    }
}