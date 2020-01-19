﻿using System.Linq;
using AgentPortal.Domain.Db;
using Microsoft.EntityFrameworkCore;

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
    }
}