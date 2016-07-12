using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App.Core.Repositories;
using App.Infrastructure.IdentityManagement;
using Microsoft.AspNet.Identity;
using User = App.Entities.IdentityManagement.User;

namespace App.Repositories.IdentityManagement
{
    public class PermissionRepository : RepositoryBase, IPermissionRepository
    {
        private ApplicationUserManager _ApplicationUserManager;
        public PermissionRepository(IMinhKhangDatabaseContext databaseContext)
            : base(databaseContext)
        {
        }

    }
}
