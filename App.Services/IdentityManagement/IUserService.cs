using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App.Services.Dtos.IdentityManagement;

namespace App.Services.IdentityManagement
{
    public interface IUserService: IService
    {
        IEnumerable<UserSummary> GetAllUser(int? page, int? pageSize, ref int? recordCount);

    }
}
