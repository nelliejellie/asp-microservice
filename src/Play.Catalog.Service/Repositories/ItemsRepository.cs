using MongoDB.Driver;
using Play.Catalog.Service.Entities;

namespace Play.Catalog.Service.Repositories
{

    public class ItemsRepository : IItemsRepository
    {
        private const String collectionName = "items";
        private readonly IMongoCollection<Item> dbcollection;
        private readonly FilterDefinitionBuilder<Item> filterBuilder = Builders<Item>.Filter;

        public ItemsRepository(IMongoDatabase database)
        {
            dbcollection = database.GetCollection<Item>(collectionName);
        }

        // get all items
        public async Task<IReadOnlyCollection<Item>> GetItemsAsync()
        {
            return await dbcollection.Find(filterBuilder.Empty).ToListAsync();
        }

        // get an item  
        public async Task<Item> GetAsync(Guid id)
        {
            FilterDefinition<Item> filter = filterBuilder.Eq(entity => entity.Id, id);
            return await dbcollection.Find(filter).FirstOrDefaultAsync();
        }

        // create an item
        public async Task CreateAsync(Item entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }
            await dbcollection.InsertOneAsync(entity);
        }

        //update an item
        public async Task UpdateAsync(Item entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }
            FilterDefinition<Item> filter = filterBuilder.Eq(existingEntity => existingEntity.Id, entity.Id);
            await dbcollection.ReplaceOneAsync(filter, entity);
        }

        // delete an item
        public async Task RemoveAsync(Guid id)
        {
            FilterDefinition<Item> filter = filterBuilder.Eq(entity => entity.Id, id);
            await dbcollection.DeleteOneAsync(filter);
        }
    }
}