using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App.Core.Repositories;
using App.Entities.IdentityManagement;


namespace App.Repositories.IdentityManagement
{
    public interface IRoleRepository : IRepository
    {
        IEnumerable<Role> GetAll(int? page, int? pageSize, ref int? recordCount);
        Role GetById(int id);
        void Create(Role role);
        void Update(Role role);
        void Delete(Role role);
    }
}
