using System.Collections.Generic;
using System.Threading.Tasks;

namespace HttpClientDemo.Repository
{
    public interface IRepository<TEntity, TKey>
        where TEntity : class, new()
    {
        Task<List<TEntity>> GetAll();

        Task<TEntity> GetByKey(TKey key);

        Task<TEntity> Add(TEntity entity);

        Task<TEntity> Update(TEntity entity);

        Task<TEntity> Delete(TKey key);
    }
}
