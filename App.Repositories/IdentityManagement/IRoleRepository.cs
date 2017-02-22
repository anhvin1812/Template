﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App.Core.Repositories;
using App.Entities.ProductManagement;


namespace App.Repositories.IdentityManagement
{
    public interface IRoleRepository : IRepository
    {
        #region Role
        IEnumerable<Role> GetAll(int? page, int? pageSize, ref int? recordCount);
        Role GetByName(string roleName);
        Role GetById(int id);
        void Insert(Role role);
        void Update(Role role);
        void Delete(Role role);

        bool RoleExists(string roleName);
        #endregion


        #region RoleClaim

        IEnumerable<RoleClaim> GetRoleClaimsByRoleId(int id);

        void InsertRoleClaim(RoleClaim entity);

        void DeleteRoleClaim(int id);

        #endregion
    }
}
