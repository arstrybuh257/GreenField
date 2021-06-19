using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GreenField.DAL.DataAccess.Interfaces;
using GreenField.DAL.Entities;
using GreenField.DAL.Repositories.Interfaces;

namespace GreenField.DAL.Repositories
{
    public class WeedRepository : IWeedRepository
    {
        private readonly IRepository<Weed> _repository;

        public WeedRepository(IRepository<Weed> repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Weed>> BrowseAsync()
        {
            return await _repository.BrowseAsync(x=>true, new object());
        }

        public async Task<Weed> GetAsync(Guid id)
        {
            return await _repository.GetAsync(id);
        }

        public async Task CreateAsync(Weed weed)
        {
            await _repository.AddAsync(weed);
        }

        public async Task UpdateAsync(Weed weed)
        {
            await _repository.UpdateAsync(weed);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _repository.DeleteAsync(id);
        }

        public async Task<bool> ExistsAsync(string name)
        {
            return await _repository.ExistsAsync(u=>u.Kind == name);
        }

        public async Task<bool> ExistsAsync(Guid id)
        {
            return await _repository.ExistsAsync(c=>c.Id == id);
        }
    }
}