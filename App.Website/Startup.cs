using App.Infrastructure.IdentityManagement;
using App.Website;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(Startup))]
namespace App.Website
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            AuthConfiguration.ConfigureAuth(app);
            //ConfigureAuth(app);
        }

        //private void ConfigureOAuthTokenGeneration(IAppBuilder app)
        //{
        //    // Configure the db context and user manager to use a single instance per request
        //    app.CreatePerOwinContext(ApplicationDbContext.Create);
        //    app.CreatePerOwinContext<App.Infrastructure.IdentityManagement.ApplicationUserManager>(App.Infrastructure.IdentityManagement.ApplicationUserManager.Create);

        //    OAuthAuthorizationServerOptions OAuthServerOptions = new OAuthAuthorizationServerOptions()
        //    {
        //        //For Dev enviroment only (on production should be AllowInsecureHttp = false)
        //        AllowInsecureHttp = true,
        //        TokenEndpointPath = new PathString("/oauth/token"),
        //        AccessTokenExpireTimeSpan = TimeSpan.FromDays(1),
        //        Provider = new CustomOAuthProvider(),
        //        AccessTokenFormat = new CustomJwtFormat("http://minhkhangstore.com")
        //    };

        //    // OAuth 2.0 Bearer Access Token Generation
        //    app.UseOAuthAuthorizationServer(OAuthServerOptions);

        //    // Assign the role manager class to owin context
        //    app.CreatePerOwinContext<ApplicationRoleManager>(ApplicationRoleManager.Create);
        //}
    }

    
}
