using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StudyWire.Infrastructure.Presistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using StudyWire.Application.Services;
using StudyWire.Application.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using StudyWire.Domain.Interfaces;
using StudyWire.Infrastructure.Repositories;
using StudyWire.Domain.Entities;
using System.Reflection;

namespace StudyWire.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            if (Environment.GetEnvironmentVariable("DOCKER_ENVIRONMENT") == "Docker")
            {
                services.AddDbContext<StudyWireDbContext>(
                    options => options.UseSqlServer(configuration.GetConnectionString("StudyWireDocker")));
            }
            else
            {
                services.AddDbContext<StudyWireDbContext>(
                    options => options.UseSqlServer(configuration.GetConnectionString("StudyWireLocal")));
            }


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
            services.AddScoped<IAdminService, AdminService>();
            services.AddScoped<INewsRepository, NewsRepository>();
            services.AddScoped<INewsService, NewsService>();
            services.AddScoped<ISchoolRepository, SchoolRepository>();
            services.AddScoped<ISchoolService, SchoolService>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddCors();
        }
    }
}
