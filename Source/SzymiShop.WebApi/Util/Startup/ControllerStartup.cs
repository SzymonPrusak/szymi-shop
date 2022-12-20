using Microsoft.AspNetCore.Mvc.NewtonsoftJson;
using Newtonsoft.Json.Serialization;

namespace SzymiShop.WebApi.Util.Startup
{
    public static class ControllerStartup
    {
        public static void AddControllersWithNewtonsoftJson(this IServiceCollection services)
        {
            services.AddControllers(opts =>
                {
                    opts.ModelMetadataDetailsProviders.Add(new NewtonsoftJsonValidationMetadataProvider());
                })
                .AddNewtonsoftJson(opts =>
                {
                    opts.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                });
        }
    }
}
