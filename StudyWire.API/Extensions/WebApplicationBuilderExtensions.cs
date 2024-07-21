using StudyWire.API.Middlewares;
using System.Runtime.CompilerServices;

namespace StudyWire.API.Extensions
{
    public static class WebApplicationBuilderExtensions
    {
        public static void AddPresentationServices(this WebApplicationBuilder builder)
        {
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddScoped<ErrorHandlingMiddleware>();
        }
    }
}
