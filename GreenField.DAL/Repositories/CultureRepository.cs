using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GreenField.DAL.DataAccess.Interfaces;
using GreenField.DAL.Entities;
using GreenField.DAL.Repositories.Interfaces;

namespace GreenField.DAL.Repositories
{
    public class CultureRepository : ICultureRepository
    {
        private readonly IRepository<Culture> _repository;

        public CultureRepository(IRepository<Culture> repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Culture>> BrowseAsync()
        {
            return await _repository.BrowseAsync(x=>true, new object());
        }

        public async Task<Culture> GetAsync(Guid id)
        {
            return await _repository.GetAsync(id);
        }

        public async Task CreateAsync(Culture culture)
        {
            await _repository.AddAsync(culture);
        }

        public async Task UpdateAsync(Culture culture)
        {
            await _repository.UpdateAsync(culture);
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