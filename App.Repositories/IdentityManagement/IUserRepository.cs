using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App.Core.Repositories;
using App.Entities.IdentityManagement;


namespace App.Repositories.IdentityManagement
{
    public interface IUserRepository : IRepository
    {
        IEnumerable<User> GetAll(string term, bool? lockoutEnabled, bool? emailConfirmed, int? page, int? pageSize, ref int? recordCount);
        User GetUserById(int id);
        void Create(User user);
        void Update(User user);
        void Delete(User user);

        IEnumerable<Role> GetRoleByUser(int useId);

        bool IsExistedEmail(string email, int? id = null);

        #region UserRole
        void DeleteRoles(IEnumerable<UserRole> entities);
        void InsertUserRole(UserRole entity);
        #endregion
    }
}
