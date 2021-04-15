using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using GreenField.DAL.Entities;

namespace GreenField.DAL.DataAccess.Interfaces
{
    public interface IRepository<TEntity> where TEntity : BaseEntity
    {
        Task<TEntity> GetAsync(Guid id);
        
        Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> predicate);
        
        Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate);
        
        Task<IEnumerable<TEntity>> BrowseAsync<TQuery>(Expression<Func<TEntity, bool>> predicate, TQuery query);
        
        Task AddAsync(TEntity entity);
        
        Task UpdateAsync(TEntity entity);
        
        Task DeleteAsync(Guid id); 
        
        Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> predicate);
    }
}