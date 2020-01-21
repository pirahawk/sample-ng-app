using System;
using System.Linq;
using System.Threading.Tasks;
using AgentPortal.Domain.Db;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace AgentPortal.Db
{
    public class AgentPortalPortalDbContextAdapter : IPortalDbContext
    {
        private readonly AgentPortalDBContext _portalDbContext;

        public AgentPortalPortalDbContextAdapter(AgentPortalDBContext portalDbContext)
        {
            _portalDbContext = portalDbContext;
        }

        public IQueryable<TEntity> Query<TEntity>(params string[] includes) where TEntity : class
        {
            var queryable = _portalDbContext.Get<TEntity>();

            if (queryable != null && includes?.Length > 0)
            {
                foreach (var include in includes)
                {
                    queryable = queryable.Include(include);
                }
            }

            return queryable;
        }

        public async Task<TEntity> Find<TEntity>(params object[] keyValues) where TEntity : class
        {
            return await _portalDbContext.FindAsync<TEntity>(keyValues);
        }

        public async Task Add<TEntity>(params TEntity[] newEntities)
        {
            if (newEntities == null) throw new ArgumentNullException(nameof(newEntities));

            foreach (var newEntity in newEntities)
            {
                await _portalDbContext.AddAsync(newEntity);
            }
        }

        public void Attach<TEntity>(TEntity entity)
        {
            EntityEntry result = _portalDbContext.Attach(entity);
        }

        public async Task<int> SaveChanges()
        {
            return await _portalDbContext.SaveChangesAsync();
        }
    }
}