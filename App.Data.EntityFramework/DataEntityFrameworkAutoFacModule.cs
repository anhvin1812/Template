using App.Core.Repositories;
using Autofac;

namespace App.Data.EntityFramework
{
    public class DataEntityFrameworkAutofacModule : Module
    {
        private const string MinhKhangConnectionName = "MinhKhang";
        private const string MinhKhang2ConnectionName = "MinhKhang2";

        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(c => new DatabaseContext(MinhKhangConnectionName)).As<IDatabaseContext>().InstancePerLifetimeScope();
            builder.Register(c => new MinhKhangDatabaseContext(MinhKhangConnectionName)).As<IMinhKhangDatabaseContext>().InstancePerLifetimeScope();


            NamedParameter MinhKhangContextParam = new NamedParameter("nameOrConnectionString", MinhKhangConnectionName);
            builder.RegisterType<MinhKhangDbContext>().WithParameter(MinhKhangContextParam).As<IDbContext>().InstancePerLifetimeScope();
            NamedParameter DatabaseContextParam = new NamedParameter("connectionString", MinhKhangConnectionName);
            builder.RegisterType<DatabaseContext>().WithParameter(DatabaseContextParam).As<IDatabaseContext>().InstancePerLifetimeScope();
        }
    }
}
