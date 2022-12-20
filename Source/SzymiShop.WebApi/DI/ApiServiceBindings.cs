using Autofac;
using SzymiShop.WebApi.Persistence;

namespace SzymiShop.WebApi.DI
{
    public class ApiServiceBindings : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(typeof(Program).Assembly)
                .Where(t => t.Name.EndsWith("Service"))
                .AsImplementedInterfaces();
        }
    }
}
