using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using GreenField.DAL.DataAccess.Interfaces;
using GreenField.DAL.Entities;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace GreenField.DAL.DataAccess.Mongo
{
    public class MongoRepository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity
    {
        protected IMongoCollection<TEntity> Collection { get; }

		public MongoRepository(IMongoDatabase database, string collectionName)
		{
			Collection = database.GetCollection<TEntity>(collectionName);
		}

		public async Task<TEntity> GetAsync(Guid id)
		{
			return await GetAsync(e => e.Id == id);
		}

		public async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> predicate)
		{
			return await Collection.Find(predicate).SingleOrDefaultAsync();
		}

		public async Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate)
		{
			return await Collection.Find(predicate).ToListAsync();
		}

		public async Task<IEnumerable<TEntity>> BrowseAsync<TQuery>(Expression<Func<TEntity, bool>> predicate, TQuery query)
		{
			return await Collection.AsQueryable().Where(predicate).ToListAsync();
		}

		public async Task AddAsync(TEntity entity)
		{
			await Collection.InsertOneAsync(entity);
		}

		public async Task UpdateAsync(TEntity entity)
		{
			await Collection.ReplaceOneAsync(e => e.Id == entity.Id, entity);
		}

		public async Task DeleteAsync(Guid id)
		{
			await Collection.DeleteOneAsync(e => e.Id == id);
		}

		public async Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> predicate)
		{
			return await Collection.Find(predicate).AnyAsync();
		}
    }
}