﻿using System.Linq;
using System.Threading.Tasks;

namespace AgentPortal.Domain.Db
{
    public interface IPortalDbContext
    {
        IQueryable<TEntity> Query<TEntity>(params string[] includes) where TEntity : class;
    }
}