using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using StudyWire.Application.DTOsModel.User.Validators;
using System.Reflection;

namespace StudyWire.Application.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddApplicationServices(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddValidatorsFromAssemblyContaining<RegisterUserDtoValidator>()
                .AddFluentValidationAutoValidation();
                
        }
    }
}
