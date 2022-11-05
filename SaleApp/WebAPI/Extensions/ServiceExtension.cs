using Domain.Models;
using Domain.UnitOfWork;
using Infrastructure.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace WebAPI.Extensions
{
    public static class ServiceExtension
    {
        public static void ConfigureCors(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            });
        }

        public static void ConfigureIISIntergration(this IServiceCollection services)
        {
            services.Configure<IISOptions>(options =>
            {

            });
        }

        public static void ConfigureSqlServiceContext(this IServiceCollection services, IConfiguration config)
        {
            var connectionString = config.GetConnectionString("MyConnectionString");
            services.AddDbContext<myDBContext>(o => o.UseSqlServer(connectionString));
        }

        public static void ConfigureRepositoryWrapper(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            //services.AddTransient<IMailService, MailService>();

        }
    }
}
