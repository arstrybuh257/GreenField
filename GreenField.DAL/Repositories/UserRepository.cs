using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GreenField.DAL.DataAccess.Interfaces;
using GreenField.DAL.Entities;
using GreenField.DAL.Repositories.Interfaces;

namespace GreenField.DAL.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IRepository<User> _repository;

        public UserRepository(IRepository<User> repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<User>> BrowseAsync()
        {
            return await _repository.BrowseAsync(x=>true, new object());
        }

        public async Task<User> GetAsync(Guid id)
        {
            return await _repository.GetAsync(id);
        }

        public async Task<User> GetByEmailAsync(string email)
        {
            return (await _repository.FindAsync(x => x.Email == email)).FirstOrDefault();
        }

        public async Task CreateAsync(User user)
        {
            await _repository.AddAsync(user);
        }

        public async Task UpdateAsync(User user)
        {
            await _repository.UpdateAsync(user);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _repository.DeleteAsync(id);
        }

        public async Task<bool> ExistsAsync(string email,Guid? organisationId = null)
        {
            return await _repository.ExistsAsync(u=>u.Email == email && (organisationId == null || u.OrganisationId == organisationId));
        }

        public async Task<bool> ExistsAsync(Guid id, Guid? organisationId = null)
        {
            return await _repository.ExistsAsync(c=>c.Id == id && (organisationId == null || c.OrganisationId == organisationId));
        }
    }
}