using AngularEshop.DataLayer.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AngularEshop.DataLayer.Repository
{
    public interface IGenericRepository<TEntity>: IDisposable where TEntity : BaseEntity
    {
        IQueryable<TEntity> GetEntitiesQuery();
        Task<TEntity> GetEntityById(long entityId);
        Task AddEntity(TEntity entity);
        void UpdateEntity(TEntity entity);
        void RemoveEntity(TEntity entity);
        Task RemoveEntity(long entityId);
        Task SaveChanges();
    }
}
