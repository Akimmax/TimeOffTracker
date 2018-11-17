using Autofac;
using TOT.Interfaces;
using TOT.Data;
using TOT.Business.Services;

namespace TOT.Bootstrap
{
    public class DependencyContainer : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<TOTUnitOfWork>()
                .As<IUnitOfWork>()
                .InstancePerLifetimeScope();

            builder.RegisterType<Mapping.AutoMapper>()
                .As<IMapper>()
                .InstancePerLifetimeScope();

            builder.RegisterAssemblyTypes(typeof(BaseService).Assembly)
               .Where(type => type.Name.EndsWith("Service"))
               .AsSelf()
               .AsImplementedInterfaces()
               .InstancePerLifetimeScope();
        }
    }
}