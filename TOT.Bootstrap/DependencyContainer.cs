using Autofac;
using TOT.Interfaces;
using TOT.Data;

namespace TOT.Bootstrap
{
    public class DependencyContainer : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<TOTUnitOfWork>()
                .As<IUnitOfWork>()
                .InstancePerLifetimeScope();           
            
        }
    }
}