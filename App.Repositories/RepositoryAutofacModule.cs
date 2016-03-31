using App.Data.EntityFramework;
using App.Repositories.IdentityManagement;
using Autofac;

namespace App.Repositories
{
    public class RepositoryAutofacModule: Module
    {
        protected override void Load(ContainerBuilder builder)
        {

            // User
            builder.RegisterType<UserRepository>().As<IUserRepository>().InstancePerDependency();
            //builder.RegisterType<SherpaRosterRepository>().Keyed<IRosterExtendedRepository>(DatabaseInstance.SherpaRoster).InstancePerDependency();
            //builder.RegisterType<GCrewRosterRepository>().Keyed<IRosterExtendedRepository>(DatabaseInstance.GCrewRoster).InstancePerDependency();
            
            builder.RegisterModule(new DataEntityFrameworkAutofacModule());
        }
    }
}
