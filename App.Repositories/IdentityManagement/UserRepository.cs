using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App.Core.Identity;
using App.Core.Repositories;
using App.Infastructure.IdentityManagement;
using App.Core.Identity;
using Microsoft.AspNet.Identity;

namespace App.Repositories.IdentityManagement
{
    public class UserRepository : RepositoryBase, IUserRepository
    {
        private ApplicationUserManager _ApplicationUserManager;
        public UserRepository(IMinhKhangDatabaseContext databaseContext)
            : base(databaseContext)
        {
        }

         protected ApplicationUserManager AppUserManager
        {
            get
            {
                return _ApplicationUserManager ?? Request.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
        }


        private IMinhKhangDatabaseContext DatabaseContext
        {
            get { return DatabaseContext as IMinhKhangDatabaseContext; }
        }

        public User GetUserById(int id)
        {
            var result = _ApplicationUserManager.Users.FirstOrDefault(x => x.Id == id);
            return result;    
        }

        public void Create(User user)
        {
            var result = _ApplicationUserManager.Create(user);
        }
        

    }
}
