﻿using System.Linq;
using System.Threading.Tasks;

namespace AgentPortal.Domain.Db
{
    public interface IPortalDbContext
    {
        IQueryable<TEntity> Query<TEntity>(params string[] includes) where TEntity : class;
        Task<int> SaveChanges();
        Task<TEntity> Find<TEntity>(params object[] keyValues) where TEntity : class;
        void Attach<TEntity>(TEntity entity);
        Task Add<TEntity>(params TEntity[] newEntities);
    }
}