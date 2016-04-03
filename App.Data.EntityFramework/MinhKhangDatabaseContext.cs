using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using App.Core.Repositories;
using App.Entities.IdentityManagement;
using Microsoft.AspNet.Identity.EntityFramework;

namespace App.Data.EntityFramework
{
    public class MinhKhangDatabaseContext : DatabaseContext, IMinhKhangDatabaseContext
    {
        public MinhKhangDatabaseContext(string connectionString) : base()
        {
            _dbContext = new MinhKhangDbContext(connectionString);
            Disposables.Add(_dbContext);
        }

        public DbContext MinhKhangDbContext
        {
            get { return (MinhKhangDbContext)_dbContext;  }
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
