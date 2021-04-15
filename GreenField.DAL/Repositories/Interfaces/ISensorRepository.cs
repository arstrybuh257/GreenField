using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GreenField.DAL.Entities;

namespace GreenField.DAL.Repositories.Interfaces
{
    public interface ISensorRepository 
    {
        public Task<IEnumerable<Sensor>> BrowseAsync();
        public Task<Sensor> GetAsync(Guid id);
        public Task CreateAsync(Sensor sensor);
        public Task UpdateAsync(Sensor sensor);
        public Task DeleteAsync(Guid id);
        public Task<bool> ExistsAsync(Guid id);
    }
}