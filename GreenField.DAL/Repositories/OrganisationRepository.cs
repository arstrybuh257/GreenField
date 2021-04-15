using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GreenField.DAL.DataAccess.Interfaces;
using GreenField.DAL.Entities;
using GreenField.DAL.Repositories.Interfaces;

namespace GreenField.DAL.Repositories
{
    public class OrganisationRepository : IOrganisationRepository
    {
        private readonly IRepository<Organisation> _repository;

        public OrganisationRepository(IRepository<Organisation> repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Organisation>> BrowseAsync()
        {
            return await _repository.BrowseAsync(x=>true, new object());
        }

        public async Task<Organisation> GetAsync(Guid id)
        {
            return await _repository.GetAsync(id);
        }

        public async Task CreateAsync(Organisation user)
        {
            await _repository.AddAsync(user);
        }

        public async Task UpdateAsync(Organisation user)
        {
            await _repository.UpdateAsync(user);
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