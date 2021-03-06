﻿using System;
using System.Threading.Tasks;
using App.Data.EntityFramework;
using App.Entities;
using App.Entities.ProductManagement;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;

namespace App.Infrastructure.IdentityManagement
{
    public class ApplicationUserManager : UserManager<User, int>
    {
        private UserManager<User, int> _userManager;
        public ApplicationUserManager(IUserStore<User,int> store)
            : base(store)
        {
            _userManager = new UserManager<User, int>(store);
        }

        public static ApplicationUserManager Create(IdentityFactoryOptions<ApplicationUserManager> options,
            IOwinContext owinContext)
        {

            var store = new UserStore<User, Role, int, UserLogin, UserRole, UserClaim>(owinContext.Get<MinhKhangDbContext>());
            store.AutoSaveChanges = false;
            var appUserManager = new ApplicationUserManager(store);


            //Configure validation logic for usernames
            //appUserManager.UserValidator = new CustomUserValidator(appUserManager)
            //{
            //    AllowOnlyAlphanumericUserNames = true,
            //    RequireUniqueEmail = true
            //};

            ////Configure validation logic for passwords
            //appUserManager.PasswordValidator = new CustomPasswordValidator
            //{
            //    RequiredLength = 6,
            //    RequireNonLetterOrDigit = true,
            //    RequireDigit = false,
            //    RequireLowercase = true,
            //    RequireUppercase = true,
            //};

            // Configure send email
            appUserManager.EmailService = new IdentityEmail();

            var dataProtectionProvider = options.DataProtectionProvider;
            if (dataProtectionProvider != null)
            {
                appUserManager.UserTokenProvider = new DataProtectorTokenProvider<User, int>(dataProtectionProvider.Create("ASP.NET Identity"))
                {
                    //Code for email confirmation and reset password life time
                    TokenLifespan = TimeSpan.FromHours(6)
                };
            }

            return appUserManager;
        }

        public override Task<IdentityResult> CreateAsync(User user, string password)
        {
            user.State = ObjectState.Added;
            var result = base.CreateAsync(user, password);

            return result;
        }

        //public override Task<IdentityResult> AddToRoleAsync(int userId, string roleName)
        //{
        //    var user = FindByIdAsync(userId);

        //    if (user == null)
        //    {
        //        throw new InvalidOperationException("Invalid user Id");
        //    }

        //    if (string.IsNullOrWhiteSpace(roleName))
        //    {
        //        throw new ArgumentException("roleName can not be null.");
        //    }

        //    //var role =  _uiOfWorkAsync.RepositoryAsync<ApplicationRole>().Queryable().SingleOrDefault(r => r.Name.ToUpper() == roleName.ToUpper());

        //    //if (role == null)
        //    //{
        //    //    throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, "{0} not found", new object[] { roleName }));
        //    //}


        //    //var userRole = new ApplicationUserRole {RoleId = role.Id, UserId = user.Id, ObjectState = ObjectState.Added};

        //    //role.Users.Add(userRole);
        //    //user.Roles.Add(userRole);
        //    //_uiOfWorkAsync.RepositoryAsync<ApplicationUserRole>().Insert(userRole);
        //}

        public void Create(User user)
        {
            user.State = ObjectState.Added;
        }
        public void Create(User user, string password)
        {
            _userManager.Create(user, password);
            user.State = ObjectState.Added;
        }

    }
}
