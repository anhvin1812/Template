using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App.Core.Repositories;
using App.Repositories.IdentityManagement;
using App.Services.Dtos.IdentityManagement;

namespace App.Services.IdentityManagement
{
    public class UserService: ServiceBase, IUserService
    {
        #region Contructor
        private IUserRepository UserRepository { get; set; }

        public UserService(IUnitOfWork unitOfWork, IUserRepository userRepository)
            : base(unitOfWork, new IRepository[] { userRepository }, new IService[] { })
        {
            UserRepository = userRepository;
        }

        #endregion

        #region Public Methods

        public IEnumerable<UserSummary> GetAllUser(int? page, int? pageSize, ref int? recordCount)
        {
            var users = UserRepository.GetAllUser(page, pageSize, ref recordCount)
                .Select(x => new UserSummary
                {
                    Id = x.Id,
                    UserName = x.UserName,
                    Email = x.Email,
                    PhoneNumber = x.PhoneNumber,
                    Roles = UserRepository.GetRoleByUser(x.Id).ToList().Select(r=> new RoleDetails {RoleName = r.Name, RoleId = r.Id})
                });
            
            return users;
        }
        #endregion




        #region Dispose
        private bool _disposed = false;

        protected override void Dispose(bool isDisposing)
        {
            if (!this._disposed)
            {
                if (isDisposing)
                {
                    UserRepository = null;
                }
                _disposed = true;
            }
            base.Dispose(isDisposing);
        }
        #endregion

    }
}
