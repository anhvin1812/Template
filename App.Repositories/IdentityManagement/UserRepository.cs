using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App.Core.Repositories;
using App.Data.EntityFramework;

namespace App.Repositories.IdentityManagement
{
    public class UserRepository : RepositoryBase, IUserRepository
    {
        public UserRepository(IMinhKhangDbContext databaseContext)
            : base(databaseContext)
        {
        }


        private IMinhKhangDbContext DatabaseContext
        {
            get { return DatabaseContext as IMinhKhangDbContext; }
        }

        public async Task CreateAsync(User user)
        {
            DatabaseContext.Insert(user);
            await DatabaseContext.SaveChangesAsync();
        }

        //Other Methods ignored for bravity

    }
}
