using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace App.Core.Repositories
{
    public interface IUnitOfWork:IDisposable
    {
        void RegisterRepositories<TRepository>(IEnumerable<IRepository> repositories) where TRepository : IRepository;

        Guid InstanceId { get; }
        int SaveChanges();
        Task<int> SaveChangesAsync();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);

    }
}
