using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GreenField.DAL.DataAccess.Interfaces;
using GreenField.DAL.Entities;
using GreenField.DAL.Repositories.Interfaces;

namespace GreenField.DAL.Repositories
{
    public class PesticideRepository : IPesticideRepository
    {
        private readonly IRepository<Pesticide> _repository;

        public PesticideRepository(IRepository<Pesticide> repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Pesticide>> BrowseAsync()
        {
            return await _repository.BrowseAsync(x=>true, new object());
        }

        public async Task<Pesticide> GetAsync(Guid id)
        {
            return await _repository.GetAsync(id);
        }

        public async Task CreateAsync(Pesticide pesticide)
        {
            await _repository.AddAsync(pesticide);
        }

        public async Task UpdateAsync(Pesticide pesticide)
        {
            await _repository.UpdateAsync(pesticide);
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