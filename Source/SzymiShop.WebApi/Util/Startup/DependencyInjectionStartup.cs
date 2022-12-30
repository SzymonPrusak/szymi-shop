using Autofac.Extensions.DependencyInjection;
using Autofac;
using Autofac.Core;

namespace SzymiShop.WebApi.Util.Startup
{
    public static class DependencyInjectionStartup
    {
        public static void SetupAutofacInjection(this IHostBuilder builder, params IModule[] modules)
        {
            builder.UseServiceProviderFactory(new AutofacServiceProviderFactory())
                .ConfigureContainer<ContainerBuilder>((bldr) =>
                {
                    foreach (var module in modules)
                        bldr.RegisterModule(module);
                });
        }
    }
}
