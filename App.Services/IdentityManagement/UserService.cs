using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using System.Web;
using System.Web.Security;
using App.Core.Configuration;
using App.Core.Exceptions;
using App.Core.Permission;
using App.Core.Repositories;
using App.Core.User;
using App.Entities;
using App.Entities.IdentityManagement;
using App.Infrastructure.Email;
using App.Infrastructure.File;
using App.Infrastructure.IdentityManagement;
using App.Repositories.IdentityManagement;
using App.Services.Common;
using App.Services.Common.Validations;
using App.Services.Dtos.Email;
using App.Services.Dtos.IdentityManagement;
using App.Services.Dtos.UI;
using Microsoft.AspNet.Identity;

namespace App.Services.IdentityManagement
{
    public class UserService: ServiceBase, IUserService
    {
        #region Contructor
        private ApplicationUserManager UserManager { get; set; }

        private IUserRepository UserRepository { get; set; }
        private IRoleRepository RoleRepository { get; set; }
        private ISecurityService SecurityService { get; set; }

        public UserService(IUnitOfWork unitOfWork, ISecurityService securityService, IUserRepository userRepository, IRoleRepository roleRepository,
            ApplicationUserManager userManager)
            : base(unitOfWork, new IRepository[] { userRepository }, new IService[] { securityService })
        {
            SecurityService = securityService;
            UserRepository = userRepository;
            RoleRepository = roleRepository;
            UserManager = userManager;
        }

        #endregion

        #region Public Methods

        public IEnumerable<UserSummary> GetAll(string term, bool? lockoutEnabled, bool? emailConfirmed, int? page, int? pageSize, ref int? recordCount)
        {
            var users = UserRepository.GetAll(term, lockoutEnabled, emailConfirmed, page, pageSize, ref recordCount);
            
            return ToDTOs(users);
        }

        public UserDetail GetById(int id)
        {
            var userId = CurrentClaimsIdentity.GetUserId();
            if (!SecurityService.HasPermission(userId, ApplicationPermissionCapabilities.USER, ApplicationPermissions.Read))
                throw new PermissionException();

            var entity = UserRepository.GetUserById(id);
            if(entity == null)
                throw new DataNotFoundException();

            return ToDTO(entity);
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

            var password = Membership.GeneratePassword(6, 2);
            User entity;

            using (var ts = new TransactionScope(TransactionScopeOption.Required))
            {
                entity = new User
                {
                    Firstname = entry.Firstname,
                    Lastname = entry.Lastname,
                    Email = entry.Email,
                    EmailConfirmed = entry.EmailConfirmed,
                    UserName = entry.Email,
                    PasswordHash = UserManager.PasswordHasher.HashPassword(password),
                    Address = entry.Address,
                    PhoneNumber = entry.PhoneNumber,
                    Gender = entry.Gender,
                    DateOfBirth = entry.DateOfBirth,
                    LockoutEnabled = entry.LockoutEnabled,
                    LockoutEndDateUtc = entry.LockoutEndDateUtc,
                    SecurityStamp = Guid.NewGuid().ToString()
                };

                    

                if (entry.ProfilePicture != null)
                {
                    var imageName = GalleryHelper.UploadProfileImage(entry.ProfilePicture);

                    entity.ProfilePicture = new Entities.FileManagement.Gallery
                    {
                        Image = imageName,
                        Thumbnail = imageName,
                        State = ObjectState.Added
                    };
                }

                UserRepository.Create(entity);
                Save();

                // roles
                if (entry.RoleIds != null && entry.RoleIds.Any())
                {
                    var roles = RoleRepository.GetByIds(entry.RoleIds).ToList();
                    foreach (var role in roles)
                    {
                        UserRepository.InsertUserRole(new UserRole {UserId = entity.Id, RoleId = role.Id});
                    }
                    Save();
                }

                ts.Complete();
            }

            // send confirmation email
            SendConfirmationEmail(entity, password);
        }

       

        public void Update(int id, UserEntry entry)
        {
            var userId = CurrentClaimsIdentity.GetUserId();
            if (!SecurityService.HasPermission(userId, ApplicationPermissionCapabilities.USER, ApplicationPermissions.Modify))
                throw new PermissionException();

            ValidateEntryData(entry);

            using (var ts = new TransactionScope(TransactionScopeOption.Required))
            {
                var entity = UserRepository.GetUserById(id);
                if (entity == null)
                    throw new DataNotFoundException();

                if (UserRepository.IsExistedEmail(entry.Email, entity.Id))
                {
                    var violations = new List<ErrorExtraInfo>
                    {
                        new ErrorExtraInfo {Code = ErrorCodeType.EmailIsUsed}
                    };
                    throw new ValidationError(violations);
                }

                entity.Firstname = entry.Firstname;
                entity.Lastname = entry.Lastname;
                entity.UserName = entry.Email;
                entity.Email = entry.Email;
                entity.Address = entry.Address;
                entity.PhoneNumber = entry.PhoneNumber;
                entity.Gender = entry.Gender;
                entity.DateOfBirth = entry.DateOfBirth;
                entity.LockoutEnabled = entry.LockoutEnabled;
                entity.EmailConfirmed = entry.EmailConfirmed;

                if (entity.LockoutEnabled && entity.LockoutEndDateUtc == null)
                {
                    entity.LockoutEndDateUtc = DateTime.UtcNow;
                }
                else if (!entity.LockoutEnabled && entity.LockoutEndDateUtc != null)
                {
                    entity.LockoutEndDateUtc = null;
                }


                // upload profile picture
                if (entry.ProfilePicture != null)
                {
                    var imageName = GalleryHelper.UploadProfileImage(entry.ProfilePicture);

                    if (entity.ProfilePicture != null)
                    {
                        GalleryHelper.DeleteProfileImage(entity.ProfilePicture.Image, entity.ProfilePicture.Thumbnail);
                        entity.ProfilePicture.Image = imageName;
                        entity.ProfilePicture.Thumbnail = imageName;
                        entity.ProfilePicture.State = ObjectState.Modified;
                    }
                    else
                    {
                        entity.ProfilePicture = new Entities.FileManagement.Gallery
                        {
                            Image = imageName,
                            Thumbnail = imageName,
                            State = ObjectState.Added
                        };
                    }
                }

                // roles
                UserRepository.DeleteRoles(entity.Roles);

                if (entry.RoleIds != null && entry.RoleIds.Any())
                {
                    var roles = RoleRepository.GetByIds(entry.RoleIds).ToList();
                    foreach (var role in roles)
                    {
                        UserRepository.InsertUserRole(new UserRole {UserId = entity.Id, RoleId = role.Id});
                    }
                }

                UserRepository.Update(entity);
                Save();

                ts.Complete();

            }
        }

        public void EnableLockout(int id)
        {
            var entity = UserRepository.GetUserById(id);
            if (entity == null)
                throw new DataNotFoundException();
        }

        public SelectListOptions GetGenderOptionsForDropdownList()
        {
            var status = Enum.GetValues(typeof(Gender)).OfType<Gender>();

            return new SelectListOptions
            {
                Items = status.Select(x => new OptionItem { Value = (int)x, Text = x.ToString() }),
            };
        }



        #region Account

        public void ChangePassword(ChangePasswordEntry entry)
        {
            var userId = CurrentClaimsIdentity.GetUserId();
            ValidateChangePasswordEntryData(entry);

            var entity = UserRepository.GetUserById(userId);
            if(entity == null)
                throw new DataNotFoundException();


            //var hashedCurrentPassword = UserManager.PasswordHasher.HashPassword(entry.CurrentPassword);
            var verifyCurrentPassword = UserManager.PasswordHasher.VerifyHashedPassword(entity.PasswordHash, entry.CurrentPassword);
            if (verifyCurrentPassword != PasswordVerificationResult.Success)
            {
                var violations = new List<ErrorExtraInfo> {new ErrorExtraInfo {Code = ErrorCodeType.CurrentPasswordNotCorrect}};
                throw new ValidationError(violations);
            }

            var hashedNewPassword = UserManager.PasswordHasher.HashPassword(entry.NewPassword);
            entity.PasswordHash = hashedNewPassword;

            UserRepository.Update(entity);
            Save();
        }

        public void ConfirmEmail(int userId, string code)
        {
            var entity = UserRepository.GetUserById(userId);
            if (entity == null)
                throw new DataNotFoundException();

            var isValidCode = UserManager.VerifyUserTokenAsync(userId, "Confirmation", code);

            if (!isValidCode.Result)
            {
                var violations = new List<ErrorExtraInfo> { new ErrorExtraInfo { Code = ErrorCodeType.InvalidCode } };
                throw new ValidationError(violations);
            }

            entity.EmailConfirmed = true;

            UserRepository.Update(entity);
            Save();
        }

        public void ResetPassword(ResetPasswordEntry entry)
        {
            throw new NotImplementedException();
        }

        #endregion
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

        private void ValidateChangePasswordEntryData(ChangePasswordEntry entry)
        {
            var violations = new List<ErrorExtraInfo>();

            if (entry == null)
            {
                violations.Add(new ErrorExtraInfo { Code = ErrorCodeType.InvalidData });

                throw new ValidationError(violations);
            }

            if (string.IsNullOrWhiteSpace(entry.NewPassword) || entry.NewPassword.Length < 6)
            {
                violations.Add(new ErrorExtraInfo { Code = ErrorCodeType.InvalidPassword, Property = "NewPassword" });
            }

            if (entry.NewPassword != entry.ConfirmPassword)
            {
                violations.Add(new ErrorExtraInfo { Code = ErrorCodeType.ConfirmationPasswordNotMatched, Property = "ConfirmPassword" });
            }

            if (violations.Any())
                throw new ValidationError(violations);
        }

        private IEnumerable<UserSummary> ToDTOs(IEnumerable<User> entities)
        {
            return entities.Select(x => new UserSummary
            {
                Id = x.Id,
                Lastname = x.Lastname,
                Firstname = x.Firstname,
                Username = x.UserName,
                Email = x.Email,
                EmailConfirmed = x.EmailConfirmed,
                PhoneNumber = x.PhoneNumber,
                Gender = (Gender?)x.Gender,
                DateOfBirth = x.DateOfBirth,
                LockoutEnabled = x.LockoutEnabled,
                Roles = UserRepository.GetRoleByUser(x.Id).Select(r=>new RoleSummary
                {
                    RoleId = r.Id,
                    RoleName = r.Name,
                    Description = r.Description
                }).ToList()
            });
        }

        private UserDetail ToDTO(User entity)
        {
            return new UserDetail
            {
                Id = entity.Id,
                Lastname = entity.Lastname,
                Firstname = entity.Firstname,
                Username = entity.UserName,
                Email = entity.Email,
                EmailConfirmed = entity.EmailConfirmed,
                PhoneNumber = entity.PhoneNumber,
                PhoneNumberConfirmed = entity.PhoneNumberConfirmed,
                Address = entity.Address,
                DateOfBirth = entity.DateOfBirth,
                Gender = (Gender?)entity.Gender,
                LockoutEnabled = entity.LockoutEnabled,
                LockoutEndDateUtc = entity.LockoutEndDateUtc,
                ProfilePicture = entity.ProfilePicture?.Thumbnail,
                Roles = UserRepository.GetRoleByUser(entity.Id).Select(x => new RoleSummary
                {
                    RoleId = x.Id,
                    RoleName = x.Name,
                    Description = x.Description
                }).ToList()
            };
        }

        private void SendConfirmationEmail(User entity, string password)
        {
            UserManager.SetTokenLifeTime(24*365);
            var confirmationToken = UserManager.GenerateEmailConfirmationToken(entity.Id);
            var mail = new ConfirmEmailMail(entity.Email, new ConfirmEmail
            {
                UserId = entity.Id,
                Firstname = entity.Firstname,
                Password = password,
                Code = confirmationToken
            });
            UserManager.ResetTokenLifeTime();

            MailSender.SendAsync(mail);
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
                    UserManager = null;
                }
                _disposed = true;
            }
            base.Dispose(isDisposing);
        }
        #endregion

    }
}
