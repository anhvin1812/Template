using System.Data.Entity;

namespace App.Infrastructure.IdentityManagement
{
    public class ApplicationIdentity 
    {
        public static void CreateIdentityUser()
        {
            Database.SetInitializer(new ApplicationDbInitializer());
        }
    }
}
