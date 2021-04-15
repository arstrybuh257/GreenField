using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GreenField.DAL.Entities;

namespace GreenField.DAL.Repositories.Interfaces
{
    public interface IFieldRepository
    {
        public Task<IEnumerable<Field>> BrowseAsync();
        public Task<Field> GetAsync(Guid id);
        public Task CreateAsync(Field field);
        public Task UpdateAsync(Field field);
        public Task DeleteAsync(Guid id);
        public Task<bool> ExistsAsync(Guid id);
    }
}