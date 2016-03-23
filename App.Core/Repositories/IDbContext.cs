using System;
using System.Data.Entity;
using System.Threading;
using System.Threading.Tasks;

namespace App.Core.Repositories
{
    public interface IDbContext : IDisposable
    {
        Guid InstanceId { get; }

        DbSet<T> Set<T>() where T : class;

        Database Database { get; }

        int SaveChanges();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
        Task<int> SaveChangesAsync();

        void SyncObjectState(object entity);
    }
}
