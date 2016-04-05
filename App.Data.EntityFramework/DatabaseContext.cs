using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App.Core.Common;
using App.Core.Repositories;
using App.Entities;

namespace App.Data.EntityFramework
{
    public class DatabaseContext : DisposableObject, IDatabaseContext
    {
        protected IDbContext _dbContext;

        public DatabaseContext()
        {
        }

        public DatabaseContext(string connectionString)
        {
            _instanceId = Guid.NewGuid();

            _dbContext = _dbContext = new MinhKhangDbContext(connectionString);
            Disposables.Add(_dbContext);
        }

        void IDatabaseContext.Update<TEntity>(TEntity entity)
        {
            _dbContext.Set<TEntity>().Attach(entity);

            ((IObjectState) entity).State = ObjectState.Modified;
        }

        TEntity IDatabaseContext.Insert<TEntity>(TEntity entity)
        {
            var result = _dbContext.Set<TEntity>().Add(entity);

            ((IObjectState)entity).State = ObjectState.Added;
            return result;
        }

        void IDatabaseContext.Delete<TEntity>(params object[] ids)
        {
            var entity = _dbContext.Set<TEntity>().Find(ids);
            ((IObjectState)entity).State = ObjectState.Deleted;
            Delete(entity);
        }

        private void Delete<TEntity>(TEntity entity) where TEntity : class 
        {
            _dbContext.Set<TEntity>().Attach(entity);
            _dbContext.Set<TEntity>().Remove(entity);
        }

        TEntity IDatabaseContext.FindById<TEntity>(params object[] ids)
        {
            return _dbContext.Set<TEntity>().Find(ids);
        }

        IQueryable<TEntity> IDatabaseContext.Get<TEntity>()
        {
            return _dbContext.Set<TEntity>();
        }

        Guid IDatabaseContext.InstanceId
        {
            get { return _instanceId; }
        }
        protected Guid _instanceId { get; set; }

        int IDatabaseContext.SaveChanges()
        {
            var result = _dbContext.SaveChanges();
            return result;
        }

        Task<int> IDatabaseContext.SaveChangesAsync()
        {
            Task<int> task = Task<int>.Factory.StartNew(() => 0);
            int i = task.Result;
            return task;
            //var result =  _dbContext.SaveChangesAsync();
            //return result;
        }

        void IDatabaseContext.CommitTransaction()
        {
            // Nothing to do here, 2 phase commit will handle it.
        }

        void IDatabaseContext.AbortTransaction()
        {
            // Nothing to do here, 2 phase commit will handle it.
        }

        IQueryable<TEntity> IDatabaseContext.Get<TEntity>(string storedProcedureName, params object[] args)
        {
            IQueryable<TEntity> result;

            var query = _dbContext.Database.SqlQuery<TEntity>(storedProcedureName, args).ToList();
            
            result = query.AsQueryable();

            return result;
        }
        
        public void BulkInsert<TEntity>(IList<TEntity> insertList, string tableName, IList<SqlBulkCopyColumnMapping> mapping, DataTable table) where TEntity : class
        {
            using (var connection = new SqlConnection(_dbContext.Database.Connection.ConnectionString))
            {
                connection.Open();
                
                using (var bulkCopy = new SqlBulkCopy(connection))
                {
                    bulkCopy.BatchSize = 100;
                    bulkCopy.DestinationTableName = tableName;

                    foreach (var columnMapping in mapping)
                    {
                        bulkCopy.ColumnMappings.Add(columnMapping);
                    }

                    bulkCopy.WriteToServer(table);
                }

                connection.Close();
            }
        }

        //public int Execute(string sqlCommand)
        //{
        //    return _dbContext.Database.ExecuteSqlCommand(sqlCommand);
        //}

        //public int Execute(string sqlCommand, params object[] args)
        //{
        //    var result = _dbContext.Database.ExecuteSqlCommand(sqlCommand, args);
        //    return result;
        //}

        private bool disposed = false;

        protected override void Dispose(bool isDisposing)
        {
            if (!this.disposed)
            {
                if (isDisposing)
                {
                    _dbContext = null;
                }
                disposed = true;
            }
            base.Dispose(isDisposing);
        }
    }
}
