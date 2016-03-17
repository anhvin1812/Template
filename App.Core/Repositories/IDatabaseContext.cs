﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Core.Repositories
{
    public interface IDatabaseContext : IDisposable
    {
        void Update<TEntity>(TEntity entity) where TEntity : class;

        TEntity Insert<TEntity>(TEntity entity) where TEntity : class;

        void Delete<TEntity>(params object[] ids) where TEntity : class;

        TEntity FindById<TEntity>(params object[] ids) where TEntity : class;

        IQueryable<TEntity> Get<TEntity>() where TEntity : class;

        Guid InstanceId { get; }

        int SaveChanges();

        void CommitTransaction();

        void AbortTransaction();
    }
}
