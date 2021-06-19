using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GreenField.DAL.Entities;

namespace GreenField.DAL.Repositories.Interfaces
{
    public interface IDroneRepository
    {
        public Task<IEnumerable<Drone>> BrowseAsync();
        public Task<Drone> GetAsync(Guid id);
        public Task CreateAsync(Drone drone);
        public Task UpdateAsync(Drone drone);
        public Task DeleteAsync(Guid id);
        public Task<bool> ExistsAsync(Guid guid, Guid id);
    }
}