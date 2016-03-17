using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;
using App.Core.Common;

namespace App.Core.Repositories
{
    public class UnitOfWork: DisposableObject, IUnitOfWork
    {

        public UnitOfWork(TransactionScopeOption transactionScopeOption = TransactionScopeOption.Required)
        {
            _transactionScopeOption = transactionScopeOption;
            InstanceId = Guid.NewGuid();
        }

        private Guid InstanceId { get; set; }

        private TransactionScopeOption _transactionScopeOption;
        private HashSet<IDatabaseContext> _databaseContexts = new HashSet<IDatabaseContext>();

        private int SaveChanges()
        {
            int result = 0;
            try
            {
                using (var transactionScope = new TransactionScope(_transactionScopeOption))
                {

                    foreach (var platformItem in _databaseContexts)
                    {
                        IDatabaseContext platformContext = platformItem;
                        result += platformContext.SaveChanges();
                    }

                    foreach (var platformItem in _databaseContexts)
                    {
                        IDatabaseContext platformContext = platformItem;
                        platformContext.CommitTransaction();
                    }

                    transactionScope.Complete();
                }
            }
            catch
            {
                foreach (IDatabaseContext platformContext in _databaseContexts)
                {
                    platformContext.AbortTransaction();
                }

                throw;
            }


            return result;
        }

        private Task<int> SaveChangesAsync()
        {
            throw new NotImplementedException();
        }

        private Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        private void RegisterRepositories<TRepository>(IEnumerable<TRepository> repositories) where TRepository : IRepository
        {
            foreach (var repositoryBase in repositories.OfType<RepositoryBase>())
            {
                _databaseContexts.Add(repositoryBase.DatabaseContext);
            }
        }

        void IUnitOfWork.RegisterRepositories<TRepository>(IEnumerable<IRepository> repositories)
        {
            RegisterRepositories(repositories);
        }

        Guid IUnitOfWork.InstanceId
        {
            get { return InstanceId; }
        }

        int IUnitOfWork.SaveChanges()
        {
            return SaveChanges();
        }

        Task<int> IUnitOfWork.SaveChangesAsync()
        {
            return SaveChangesAsync();
        }

        Task<int> IUnitOfWork.SaveChangesAsync(CancellationToken cancellationToken)
        {
            return SaveChangesAsync(cancellationToken);
        }

        private bool disposed = false;

        protected override void Dispose(bool isDisposing)
        {
            
            if (!this.disposed)
            {
                if (isDisposing)
                {
                    _databaseContexts = null;
                }
                disposed = true;
            }
            base.Dispose(isDisposing);
        }

    }
}
