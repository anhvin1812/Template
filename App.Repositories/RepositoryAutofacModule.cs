using App.Data.EntityFramework;
using App.Repositories.IdentityManagement;
using App.Repositories.ProductManagement;
using Autofac;

namespace App.Repositories
{
    public class RepositoryAutofacModule: Module
    {
        protected override void Load(ContainerBuilder builder)
        {

            // User
            builder.RegisterType<UserRepository>().As<IUserRepository>().InstancePerDependency();
            builder.RegisterType<RoleRepository>().As<IRoleRepository>().InstancePerDependency();
            builder.RegisterType<PermissionRepository>().As<IPermissionRepository>().InstancePerDependency();
            // Product
            builder.RegisterType<ProductRepository>().As<IProductRepository>().InstancePerDependency();
            builder.RegisterType<ProductCategoryRepository>().As<IProductCategoryRepository>().InstancePerDependency();
            builder.RegisterType<GalleryRepository>().As<IGalleryRepository>().InstancePerDependency();

            //builder.RegisterType<SherpaRosterRepository>().Keyed<IRosterExtendedRepository>(DatabaseInstance.SherpaRoster).InstancePerDependency();
            //builder.RegisterType<GCrewRosterRepository>().Keyed<IRosterExtendedRepository>(DatabaseInstance.GCrewRoster).InstancePerDependency();

            builder.RegisterModule(new DataEntityFrameworkAutofacModule());
        }
    }
}
