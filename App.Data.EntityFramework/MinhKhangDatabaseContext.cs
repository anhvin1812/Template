using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using App.Core.Repositories;
using App.Entities.ProductManagement;
using Microsoft.AspNet.Identity.EntityFramework;

namespace App.Data.EntityFramework
{
    public class MinhKhangDatabaseContext : DatabaseContext, IMinhKhangDatabaseContext
    {
        public MinhKhangDatabaseContext(string connectionString) : base(connectionString)
        {
        }

        public DbContext MinhKhangDbContext => (MinhKhangDbContext)_dbContext;

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
