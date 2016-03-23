using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App.Data.EntityFramework.Mapping;
using App.Infastructure.IdentityManagement;

namespace App.Data.EntityFramework
{
    public class MinhKhangDbContext : DbContextBase, IMinhKhangDbContext
    {
        public MinhKhangDbContext(string nameOrConnectionString) :
            base(nameOrConnectionString)
        {
            Database.SetInitializer<MinhKhangDbContext>(null);
            //Configuration.ProxyCreationEnabled = true;
            //Configuration.LazyLoadingEnabled = true;

            // Sets DateTimeKinds on DateTimes of Entities, so that Dates are automatically set to be UTC.
            // Currently only processes CleanEntityBase entities. All EntityBase entities remain unchanged.
            // http://stackoverflow.com/questions/4648540/entity-framework-datetime-and-utc
           // ((IObjectContextAdapter)this).ObjectContext.ObjectMaterialized += (sender, e) => DateTimeKindAttribute.Apply(e.Entity);
        }

        public new IDbSet<T> Set<T>() where T : class
        {
            return base.Set<T>();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            #if DEBUG
            Database.Log = s => Debug.Write(s);
            #endif

            // Add entities for Identity
            modelBuilder.Entity<UserRole>().ToTable("UserRole", "Security");
            modelBuilder.Entity<UserLogin>().ToTable("UserLogin", "Security");
            modelBuilder.Entity<UserClaim>().ToTable("UserClaim", "Security");
            modelBuilder.Entity<Role>().ToTable("Role", "Security");
            modelBuilder.Entity<User>().ToTable("User", "Security");

            // Mapping
            IdentityManagementMap.Configure(modelBuilder);

        }

        private bool disposed = false;

        protected override void Dispose(bool isDisposing)
        {
            if (!this.disposed)
            {
                if (isDisposing)
                {
                }
                disposed = true;
            }
            base.Dispose(isDisposing);
        }
    }
}
