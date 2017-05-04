using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App.Core.Exceptions;
using App.Core.Permission;
using App.Core.Repositories;
using App.Entities.ProductManagement;
using App.Repositories.IdentityManagement;
using App.Services.Common;
using App.Services.Common.Validations;
using App.Services.Dtos.IdentityManagement;

namespace App.Services.IdentityManagement
{
    public class UserService: ServiceBase, IUserService
    {
        #region Contructor
        private IUserRepository UserRepository { get; set; }
        private ISecurityService SecurityService { get; set; }

        public UserService(IUnitOfWork unitOfWork, ISecurityService securityService, IUserRepository userRepository)
            : base(unitOfWork, new IRepository[] { userRepository }, new IService[] { securityService })
        {
            SecurityService = securityService;
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
                    Firstname = x.Firstname,
                    Lastname = x.Lastname,
                    Username = x.UserName,
                    Email = x.Email,
                    PhoneNumber = x.PhoneNumber,
                    Roles = UserRepository.GetRoleByUser(x.Id).ToList().Select(r=> new RoleDetails {RoleName = r.Name, RoleId = r.Id})
                });
            
            return users;
        }

        public UserDetail GetById(int id)
        {
            var userId = CurrentClaimsIdentity.GetUserId();
            if (!SecurityService.HasPermission(userId, ApplicationPermissionCapabilities.USER, ApplicationPermissions.Read))
                throw new PermissionException();

            var entity = UserRepository.GetUserById(id);
            if(entity == null)
                throw new DataNotFoundException();

            return new UserDetail {
                Id = entity.Id,
                Lastname = entity.Lastname,
                Firstname = entity.Firstname,
                Email = entity.Email,
                EmailConfirmed = entity.EmailConfirmed,
                PhoneNumber = entity.PhoneNumber,
                PhoneNumberConfirmed = entity.PhoneNumberConfirmed,
                Address = entity.Address,
                LockoutEnabled = entity.LockoutEnabled,
                LockoutEndDateUtc = entity.LockoutEndDateUtc,
                Username = entity.UserName,
                Roles = UserRepository.GetRoleByUser(id).Select(x=>new RoleSummary
                {
                    RoleId = x.Id,
                    RoleName = x.Name,
                    Description = x.Description
                }).ToList()
            };
        }

        public void Insert(UserEntry entry)
        {
            var userId = CurrentClaimsIdentity.GetUserId();
            if (!SecurityService.HasPermission(userId, ApplicationPermissionCapabilities.USER, ApplicationPermissions.Create))
                throw new PermissionException();

            ValidateEntryData(entry);

            if(UserRepository.IsExistedEmail(entry.Email))
            {
                var violations = new List<ErrorExtraInfo> {
                    new ErrorExtraInfo { Code = ErrorCodeType.EmailIsUsed }
                };
                throw new ValidationError(violations);
            }

            var entity = new User
            {
                Firstname = entry.Firstname,
                Lastname = entry.Lastname,
                Email = entry.Email,
                Address = entry.Address,
                PhoneNumber = entry.PhoneNumber,
                
            };

            UserRepository.Create(entity);
            Save();

        }

        public void Update(int id, UserEntry entry)
        {
            var entity = UserRepository.GetUserById(id);
            if (entity == null)
                throw new DataNotFoundException();
        }

        public void EnableLockout(int id)
        {
            var entity = UserRepository.GetUserById(id);
            if (entity == null)
                throw new DataNotFoundException();
        }


        #endregion

        #region Private Methods
        private void ValidateEntryData(UserEntry entry)
        {
            var violations = new List<ErrorExtraInfo>();

            if (entry == null)
            {
                violations.Add(new ErrorExtraInfo { Code = ErrorCodeType.InvalidData });
               
                throw new ValidationError(violations);
            }

            if (string.IsNullOrWhiteSpace(entry.Firstname))
            {
                violations.Add(new ErrorExtraInfo { Code = ErrorCodeType.InvalidFirstName, Property = "Firstname" });
            }

            if (string.IsNullOrWhiteSpace(entry.Lastname))
            {
                violations.Add(new ErrorExtraInfo { Code = ErrorCodeType.InvalidLastName, Property = "LastName" });
            }

            if (!ValidationHelper.IsEmail(entry.Email))
            {
                violations.Add(new ErrorExtraInfo { Code = ErrorCodeType.InvalidEmail, Property = "Email" });
            }

            if (violations.Any())
                throw new ValidationError(violations);
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
                    SecurityService = null;
                    UserRepository = null;
                }
                _disposed = true;
            }
            base.Dispose(isDisposing);
        }
        #endregion

    }
}
