using MongoDB.Driver;
using Play.Catalog.Service.Entities;

namespace Play.Catalog.Service.Repositories
{

    public class ItemsRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly IMongoCollection<T> dbcollection;
        private readonly FilterDefinitionBuilder<T> filterBuilder = Builders<T>.Filter;

        public ItemsRepository(IMongoDatabase database, string collectionName)
        {
            dbcollection = database.GetCollection<T>(collectionName);
        }

        // get all items
        public async Task<IReadOnlyCollection<T>> GetItemsAsync()
        {
            return await dbcollection.Find(filterBuilder.Empty).ToListAsync();
        }

        // get an item  
        public async Task<T> GetAsync(Guid id)
        {
            FilterDefinition<T> filter = filterBuilder.Eq(entity => entity.id, id);
            return await dbcollection.Find(filter).FirstOrDefaultAsync();
        }

        // create an item
        public async Task CreateAsync(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }
            await dbcollection.InsertOneAsync(entity);
        }

        //update an item
        public async Task UpdateAsync(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }
            FilterDefinition<T> filter = filterBuilder.Eq(existingEntity => existingEntity.id, entity.id);
            await dbcollection.ReplaceOneAsync(filter, entity);
        }

        // delete an item
        public async Task RemoveAsync(Guid id)
        {
            FilterDefinition<T> filter = filterBuilder.Eq(entity => entity.id, id);
            await dbcollection.DeleteOneAsync(filter);
        }
    }
}