using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App.Core.Repositories;
using App.Entities.ProductManagement;


namespace App.Repositories.IdentityManagement
{
    public interface IUserRepository : IRepository
    {
        IEnumerable<User> GetAllUser(int? page, int? pageSize, ref int? recordCount);
        User GetUserById(int id);
        void Create(User user);
        void Update(User user);
        void Delete(User user);

        IEnumerable<Role> GetRoleByUser(int useId);
    }
}
