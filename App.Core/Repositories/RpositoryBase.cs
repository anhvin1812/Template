using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using App.Core.Common;

namespace App.Core.Repositories
{
    public abstract class RepositoryBase : DisposableObject, IRepository
    {
        public DateTime NullDateTime = DateTime.Parse("1/1/1900");

        /// <summary>
        /// Initializes a new instance of the <see cref="RepositoryBase"/> class.
        /// </summary>
        /// <param name="DatabaseContext">The platform context.</param>
        /// <remarks>
        /// Concrete repositories should inject more specific interfaces that inherit from IDatabaseContext.
        /// </remarks>
        protected RepositoryBase(IDatabaseContext DatabaseContext)
        {
            DatabaseContext = DatabaseContext;
            InstanceId = Guid.NewGuid();
        }

        protected ClaimsPrincipal CurrentClaimsIdentity { get; private set; }
        public void SetIdentity(ClaimsPrincipal identity)
        {
            CurrentClaimsIdentity = identity;
        }

        /// <summary>
        /// Gets or sets the platform context.
        /// </summary>
        /// <value>
        /// The platform context.
        /// </value>
        /// <remarks>
        /// This property should be visible to UnitOfWork to enable cross DatabaseContexts transaction management.
        /// </remarks>
        protected internal IDatabaseContext DatabaseContext { get; set; }

        private Guid InstanceId { get; set; }

        Guid IRepository.InstanceId
        {
            get { return InstanceId; }
        }

        private bool disposed = false;

        protected override void Dispose(bool isDisposing)
        {
            if (!this.disposed)
            {
                if (isDisposing)
                {
                    DatabaseContext = null;
                }
                disposed = true;
            }
            base.Dispose(isDisposing);
        }
    }
}
