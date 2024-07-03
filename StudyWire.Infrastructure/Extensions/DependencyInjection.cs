using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StudyWire.Infrastructure.Presistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using StudyWire.Application.Services;
using StudyWire.Application.Services.Interfaces;
using StudyWire.Application.Middlewares;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using StudyWire.Domain.Interfaces;
using StudyWire.Infrastructure.Repositories;
using StudyWire.Domain.Entities;
using System.Reflection;

namespace StudyWire.Infrastructure.Extensions
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            Configuration.SetConfiguration(configuration);
            services.AddDbContext<StudyWireDbContext>(
                options => options.UseSqlServer(configuration.GetConnectionString("StudyWire")));

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding
                            .UTF8.GetBytes(configuration.GetRequiredSection("TokenKey").Value)),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                });

            services.AddIdentityCore<AppUser>()
                    .AddRoles<IdentityRole<int>>()
                    .AddEntityFrameworkStores<StudyWireDbContext>();
            
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IAdminService, AdminService>();
            services.AddScoped<INewsRepository, NewsRepository>();
            services.AddScoped<INewsService, NewsService>();
            services.AddScoped<ErrorHandlingMiddleware>();
            services.AddCors();


            return services;
        }
    }
}
