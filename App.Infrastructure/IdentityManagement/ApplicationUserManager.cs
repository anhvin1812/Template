using System;
using System.Data.Entity;
using System.Threading.Tasks;
using App.Data.EntityFramework;
using App.Entities;
using App.Entities.IdentityManagement;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security.DataProtection;

namespace App.Infrastructure.IdentityManagement
{
    public class ApplicationUserManager : UserManager<User, int>
    {
        private UserManager<User, int> _userManager;
        public ApplicationUserManager(IUserStore<User,int> store)
            : base(store)
        {
            _userManager = new UserManager<User, int>(store);

            SetTokenLifeTime(36);
        }

        public void SetTokenLifeTime(double hours)
        {
            var dataProtectionProvider = AuthConfiguration.DataProtectionProvider;

            this.UserTokenProvider = new DataProtectorTokenProvider<User, int>(dataProtectionProvider.Create("ASP.NET Identity"))
            {
                //Code for email confirmation and reset password life time
                TokenLifespan = TimeSpan.FromHours(hours)
            };
        }

        public void ResetTokenLifeTime()
        {
            SetTokenLifeTime(36);
        }


        public static ApplicationUserManager Create(IdentityFactoryOptions<ApplicationUserManager> options,
            IOwinContext context)
        {

            var store = new UserStore<User, Role, int, UserLogin, UserRole, UserClaim>(context.Get<MinhKhangDbContext>());
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

            var dataProtectionProvider = new DpapiDataProtectionProvider();

            appUserManager.UserTokenProvider =
                new DataProtectorTokenProvider<User, int>(dataProtectionProvider.Create("ASP.NET Identity"))
                {
                    //Code for email confirmation and reset password life time
                    TokenLifespan = TimeSpan.FromHours(48)
                };

            return appUserManager;
        }

        public static ApplicationUserManager Create(DbContext context)
        {

            var store = new UserStore<User, Role, int, UserLogin, UserRole, UserClaim>(context);
            store.AutoSaveChanges = true;
            var appUserManager = new ApplicationUserManager(store);
            appUserManager._userManager = new UserManager<User, int>(store);
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
            user.State = ObjectState.Added;
            _userManager.Create(user, password);
            
        }

    }
}
