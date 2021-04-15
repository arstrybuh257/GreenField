using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GreenField.DAL.Entities;

namespace GreenField.DAL.Repositories.Interfaces
{
    public interface IPesticideRepository
    {
        public Task<IEnumerable<Pesticide>> BrowseAsync();
        public Task<Pesticide> GetAsync(Guid id);
        public Task CreateAsync(Pesticide pesticide);
        public Task UpdateAsync(Pesticide pesticide);
        public Task DeleteAsync(Guid id);
        public Task<bool> ExistsAsync(string kind);
        public Task<bool> ExistsAsync(Guid id);
    }
}