using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GreenField.DAL.DataAccess.Interfaces;
using GreenField.DAL.Entities;
using GreenField.DAL.Repositories.Interfaces;

namespace GreenField.DAL.Repositories
{
    public class SensorRepository : ISensorRepository
    {
        private readonly IRepository<Sensor> _repository;

        public SensorRepository(IRepository<Sensor> repository)
        {
            _repository = repository;
        }
        
        public async Task<IEnumerable<Sensor>> BrowseAsync()
        {
            return await _repository.BrowseAsync(x=>true, new object());
        }

        public async Task<Sensor> GetAsync(Guid id)
        {
            return await _repository.GetAsync(id);
        }

        public async Task CreateAsync(Sensor sensor)
        {
            await _repository.AddAsync(sensor);
        }

        public async Task UpdateAsync(Sensor sensor)
        {
            await _repository.UpdateAsync(sensor);
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