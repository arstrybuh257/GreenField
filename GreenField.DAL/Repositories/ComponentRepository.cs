using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GreenField.DAL.DataAccess.Interfaces;
using GreenField.DAL.Entities;
using GreenField.DAL.Repositories.Interfaces;

namespace GreenField.DAL.Repositories
{
    public class ComponentRepository : IComponentRepository
    {
        private readonly IRepository<Component> _repository;

        public ComponentRepository(IRepository<Component> repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Component>> BrowseAsync()
        {
            return await _repository.BrowseAsync(x=>true, new object());
        }

        public async Task<Component> GetAsync(Guid id)
        {
            return await _repository.GetAsync(id);
        }

        public async Task CreateAsync(Component component)
        {
            await _repository.AddAsync(component);
        }

        public async Task UpdateAsync(Component component)
        {
            await _repository.UpdateAsync(component);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _repository.DeleteAsync(id);
        }

        public async Task<bool> ExistsAsync(string name)
        {
            return await _repository.ExistsAsync(u=>u.Name == name);
        }

        public async Task<bool> ExistsAsync(Guid id)
        {
            return await _repository.ExistsAsync(c=>c.Id == id);
        }
    }
}