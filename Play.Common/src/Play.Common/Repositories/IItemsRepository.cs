using Play.Common.Entities;

namespace Play.Common.Repositories
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