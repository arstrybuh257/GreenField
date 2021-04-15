using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GreenField.DAL.DataAccess.Interfaces;
using GreenField.DAL.Entities;
using GreenField.DAL.Repositories.Interfaces;

namespace GreenField.DAL.Repositories
{
    public class DroneRepository : IDroneRepository
    {
        private readonly IRepository<Drone> _repository;

        public DroneRepository(IRepository<Drone> repository)
        {
            _repository = repository;
        }
        
        public async Task<IEnumerable<Drone>> BrowseAsync()
        {
            return await _repository.BrowseAsync(x=>true, new object());
        }

        public async Task<Drone> GetAsync(Guid id)
        {
            return await _repository.GetAsync(id);
        }

        public async Task CreateAsync(Drone drone)
        {
            await _repository.AddAsync(drone);
        }

        public async Task UpdateAsync(Drone drone)
        {
            await _repository.UpdateAsync(drone);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _repository.DeleteAsync(id);
        }

        public async Task<bool> ExistsAsync(Guid id)
        {
            return await _repository.ExistsAsync(c=>c.Id == id);
        }
    }
}