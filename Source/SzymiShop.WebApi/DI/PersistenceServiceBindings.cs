using Autofac;
using SzymiShop.WebApi.Persistence;

namespace SzymiShop.WebApi.DI
{
    public class PersistenceServiceBindings : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(typeof(ShopDbContext).Assembly)
                .Where(t => t.Name.EndsWith("Service"))
                .AsImplementedInterfaces();
        }
    }
}
