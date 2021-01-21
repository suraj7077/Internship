using API.data;
using API.interfaces;
using API.Services;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Section6.dattingapp.API.data;
using Section6.dattingapp.API.Helpers;
using Section6.dattingapp.API.interfaces;
using Section6.dattingapp.API.Services;

namespace API.Extensions
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
        {
            services.Configure<CloudinarySettings>(config.GetSection("CloudinarySettings"));
            services.AddScoped<ITokenService,TokenService>();
            services.AddScoped<IPhotoService,PhotoService>();
            services.AddScoped<LogUserActivity>();
            services.AddScoped<IUserRepository,UserRepository>();
            services.AddAutoMapper(typeof(AutoMapperProfiles).Assembly);
            services.AddDbContext<DataContext>(options =>
            {
                options.UseSqlite(config.GetConnectionString("DefaultConnection"));
            });
            return services;
        }
    }
}