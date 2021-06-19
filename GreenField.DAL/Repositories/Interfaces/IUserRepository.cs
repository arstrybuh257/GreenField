using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GreenField.DAL.Entities;

namespace GreenField.DAL.Repositories.Interfaces
{
    public interface IUserRepository
    {
        public Task<IEnumerable<User>> BrowseAsync();
        public Task<User> GetAsync(Guid id);
        public Task<User> GetByEmailAsync(string email);
        public Task CreateAsync(User user);
        public Task UpdateAsync(User user);
        public Task DeleteAsync(Guid id);
        public Task<bool> ExistsAsync(string email, Guid? organisationId);
        public Task<bool> ExistsAsync(Guid id, Guid? organisationId);
    }
}