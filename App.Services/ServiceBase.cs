using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using App.Core.Common;
using App.Core.Repositories;
using App.Services.EntityMapping;

namespace App.Services
{
    /// <summary>
    /// Base Service Implementation
    /// </summary>
    public abstract class ServiceBase : DisposableObject, IService
    {
        static ServiceBase()
        {
            MappingHelper.InitializeMapping();
        }

        protected ServiceBase(IUnitOfWork unitOfWork = null, IEnumerable<IRepository> repositories = null, IEnumerable<IService> sevices = null)
        {
            ServiceBaseInit(unitOfWork, repositories, sevices);
        }

        private void ServiceBaseInit(IUnitOfWork unitOfWork, IEnumerable<IRepository> repositories, IEnumerable<IService> sevices)
        {
            OwnsUnitOfWork = true;
            UnitOfWork = unitOfWork;

            Services = new HashSet<IService>();
            Repositories = new HashSet<RepositoryBase>();

            if (sevices != null)
            {
                foreach (var service in sevices)
                {
                    RegisterService(service);
                }
            }

            if (repositories != null && UnitOfWork != null)
            {
                var repositoriesToRegister = repositories.OfType<RepositoryBase>().ToList();
                UnitOfWork.RegisterRepositories<RepositoryBase>(repositoriesToRegister);
                foreach (var repository in repositoriesToRegister)
                {
                    Repositories.Add(repository);
                }
            }
        }

        //protected ServiceBase(IUnitOfWork unitOfWork, IIndex<DatabaseInstance, IRosterExtendedRepository> extendedRepositories, IEnumerable<IRepository> repositories = null,
        //    IEnumerable<IService> sevices = null)
        //{
        //    ServiceBaseInit(unitOfWork, repositories, sevices);

        //    if (extendedRepositories != null && UnitOfWork != null)
        //    {
        //        var repositoriesToRegisters = new List<RepositoryBase>();
        //        var databaseInstances = Enum.GetValues(typeof(DatabaseInstance));
        //        foreach (var database in databaseInstances)
        //        {
        //            var extendedRepository = extendedRepositories[(DatabaseInstance)database];
        //            repositoriesToRegisters.Add((RepositoryBase)extendedRepository);
        //        }

        //        UnitOfWork.RegisterRepositories<RepositoryBase>(repositoriesToRegisters);
        //        foreach (var repository in repositoriesToRegisters)
        //        {
        //            Repositories.Add(repository);
        //        }
        //    }
        //}

        protected ClaimsPrincipal CurrentClaimsIdentity { get; private set; }

        public virtual void SetIdentity(ClaimsPrincipal identity)
        {
            CurrentClaimsIdentity = identity;
            if (Services == null) Services = new HashSet<IService>();
            foreach (var service in Services)
            {
                service.SetIdentity(identity);
            }

            foreach (var repository in Repositories)
            {
                repository.SetIdentity(identity);
            }
        }

        protected virtual void Save(bool saveImmediately = false)
        {
            if (OwnsUnitOfWork || saveImmediately) UnitOfWork.SaveChanges();
        }

        protected virtual Task<int> SaveAsync(bool saveImmediately = false)
        {
            return !OwnsUnitOfWork && !saveImmediately ? Task.FromResult(0) : UnitOfWork.SaveChangesAsync();
        }

        protected virtual Task<int> SaveAsync(CancellationToken cancellationToken, bool saveImmediately = false)
        {
            return !OwnsUnitOfWork && !saveImmediately ? Task.FromResult(0) : UnitOfWork.SaveChangesAsync(cancellationToken);
        }

        protected TService GetService<TService>()
            where TService : class, IService
        {
            var service = Services.FirstOrDefault(s => s as TService != null) as TService;
            return service;
        }

        protected void RegisterService<TService>(TService service)
            where TService : class, IService
        {
            var baseService = service as ServiceBase;
            if (baseService != null)
            {
                baseService.OwnsUnitOfWork = false;
            }
            Services.Add(service);
        }

        protected void RegisterRepository<TRepository>(TRepository repository)
            where TRepository : RepositoryBase, IRepository
        {
            Repositories.Add(repository);
        }

        public void SetClaim(string claim, string value)
        {
            SetClaim(new Claim(claim, value));
        }

        public void SetClaim(Claim claim)
        {
            if (CurrentClaimsIdentity.FindFirst(claim.Type) != null)
            {
                RemoveClaim(claim.Type);
            }
            var claims = new List<Claim> { claim };
            var identity = new ClaimsIdentity(claims);

            CurrentClaimsIdentity.AddIdentity(identity);
        }

        public void RemoveClaim(String claimType)
        {
            if (CurrentClaimsIdentity.FindFirst(claimType) != null)
            {
                var identity = CurrentClaimsIdentity.Identity as ClaimsIdentity;
                if (identity != null)
                {
                    var claim = (from c in CurrentClaimsIdentity.Claims
                                 where c.Type == claimType
                                 select c).Single();
                    identity.RemoveClaim(claim);
                }
            }
        }

        private HashSet<IService> Services { get; set; }
        private HashSet<RepositoryBase> Repositories { get; set; }

        private IUnitOfWork UnitOfWork { get; set; }

        protected bool OwnsUnitOfWork { get; private set; }

        private bool disposed;

        protected override void Dispose(bool isDisposing)
        {
            if (!disposed)
            {
                if (isDisposing)
                {
                    UnitOfWork = null;
                    Services = null;
                    Repositories = null;
                    CurrentClaimsIdentity = null;
                }
                disposed = true;
            }
            base.Dispose(isDisposing);
        }

    }
}
