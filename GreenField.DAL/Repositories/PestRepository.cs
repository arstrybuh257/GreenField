using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GreenField.DAL.DataAccess.Interfaces;
using GreenField.DAL.Entities;
using GreenField.DAL.Repositories.Interfaces;

namespace GreenField.DAL.Repositories
{
    public class PestRepository : IPestRepository
    {
        private readonly IRepository<Pest> _repository;

        public PestRepository(IRepository<Pest> repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Pest>> BrowseAsync()
        {
            return await _repository.BrowseAsync(x=>true, new object());
        }

        public async Task<Pest> GetAsync(Guid id)
        {
            return await _repository.GetAsync(id);
        }

        public async Task CreateAsync(Pest pest)
        {
            await _repository.AddAsync(pest);
        }

        public async Task UpdateAsync(Pest pest)
        {
            await _repository.UpdateAsync(pest);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _repository.DeleteAsync(id);
        }

        public async Task<bool> ExistsAsync(string kind)
        {
            return await _repository.ExistsAsync(u=>u.Kind == kind);
        }

        public async Task<bool> ExistsAsync(Guid id)
        {
            return await _repository.ExistsAsync(c=>c.Id == id);
        }
    }
}