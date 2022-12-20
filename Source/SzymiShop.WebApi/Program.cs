using Autofac;
using Autofac.Extensions.DependencyInjection;
using SzymiShop.WebApi.DI;
using SzymiShop.WebApi.Util.Startup;

namespace SzymiShop.WebApi
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllersWithNewtonsoftJson();

            builder.Services.AddPostgresDatabase(builder.Configuration);

            builder.Services.AddJwtBearerAuthentication(builder.Configuration);

            SetupInjection(builder);

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthentication();

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }


        private static void SetupInjection(WebApplicationBuilder builder)
        {
            builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory())
                .ConfigureContainer<ContainerBuilder>((bldr) =>
                {
                    bldr.RegisterModule(new PersistenceServiceBindings());
                    bldr.RegisterModule(new ApiServiceBindings());
                });
        }
    }
}