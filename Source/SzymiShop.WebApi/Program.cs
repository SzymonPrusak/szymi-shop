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

            builder.Host.SetupAutofacInjection(
                new PersistenceServiceBindings(),
                new ApiServiceBindings()
            );

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddCors(opts =>
                opts.AddDefaultPolicy(bldr =>
                    bldr.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                    )
                );

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseCors();

            app.UseAuthentication();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}