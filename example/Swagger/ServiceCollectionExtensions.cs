using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Byndyusoft.AspNetCore.Mvc.Formatters.ProtoBuf.Swagger
{
    /// <summary>
    ///     ServiceCollectionExtensions
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        ///     AddSwagger
        /// </summary>
        public static IServiceCollection AddSwagger(this IServiceCollection services)
        {
            return
                services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>()
                        .AddSwaggerGen();
        }
    }
}