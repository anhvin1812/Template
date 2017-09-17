using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App.Services.Dtos.IdentityManagement;
using App.Services.Dtos.UI;

namespace App.Services.IdentityManagement
{
    public interface IUserService: IService
    {
        IEnumerable<UserSummary> GetAll(string term, bool? lockoutEnabled, bool? emailConfirmed, int? page, int? pageSize, ref int? recordCount);
        UserDetail GetById(int id);

        void Insert(UserEntry entry);

        void Update(int id, UserEntry entry);

        void EnableLockout(int id);

        SelectListOptions GetGenderOptionsForDropdownList();

        #region Account

        void ChangePassword(ChangePasswordEntry entry);

        void ResetPassword(ResetPasswordEntry entry);

        void ConfirmEmail(int userId, string code);

        #endregion
    }
}
