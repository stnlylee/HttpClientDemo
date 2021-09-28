using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpClientDemo.Repository
{
    /// Normally this class should implement all common CRUD functions for database
    /// eg, EntityFramework can work with generic entity 
    /// But for the demo the data source is not generic so I have to implement in child class
    [ExcludeFromCodeCoverage]
    public abstract class RepositoryBase<TEntity, TKey>
        : IRepository<TEntity, TKey>
        where TEntity : class, new()
    {
        public virtual Task<List<TEntity>> GetAll()
        {
            throw new NotImplementedException();
        }

        public virtual Task<TEntity> GetByKey(TKey key)
        {
            throw new NotImplementedException();
        }

        public Task<TEntity> Add(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public Task<TEntity> Update(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public Task<TEntity> Delete(TKey key)
        {
            throw new NotImplementedException();
        }
    }
}
