using Play.Catalog.Service.Entities;

namespace Play.Catalog.Service.Repositories
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        Task CreateAsync(T entity);
        Task<T> GetAsync(Guid id);
        Task<IReadOnlyCollection<T>> GetItemsAsync();
        Task RemoveAsync(Guid id);
        Task UpdateAsync(T entity);
    }
}