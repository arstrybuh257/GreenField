using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GreenField.DAL.DataAccess.Interfaces;
using GreenField.DAL.Entities;
using GreenField.DAL.Repositories.Interfaces;

namespace GreenField.DAL.Repositories
{
    public class FieldRepository : IFieldRepository
    {
        private readonly IRepository<Field> _repository;

        public FieldRepository(IRepository<Field> repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Field>> BrowseAsync()
        {
            return await _repository.BrowseAsync(x=>true, new object());
        }

        public async Task<Field> GetAsync(Guid id)
        {
            return await _repository.GetAsync(id);
        }

        public async Task CreateAsync(Field field)
        {
            await _repository.AddAsync(field);
        }

        public async Task UpdateAsync(Field field)
        {
            await _repository.UpdateAsync(field);
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