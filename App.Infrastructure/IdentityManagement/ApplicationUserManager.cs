using System;
using App.Data.EntityFramework;
using App.Entities.IdentityManagement;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;

namespace App.Infrastructure.IdentityManagement
{
    public class ApplicationUserManager : UserManager<User, int>
    {
        public ApplicationUserManager(IUserStore<User,int> store)
            : base(store)
        {
        }

        public static ApplicationUserManager GetUserManager(IdentityFactoryOptions<ApplicationUserManager> options,
            IOwinContext owinContext)
        {

            var store = new UserStore<User, Role, int, UserLogin, UserRole, UserClaim>(owinContext.Get<MinhKhangDbContext>());
            store.AutoSaveChanges = true;
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
    }
}
