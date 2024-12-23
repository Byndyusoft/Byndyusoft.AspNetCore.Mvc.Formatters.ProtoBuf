using Asp.Versioning.ApiExplorer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace Byndyusoft.AspNetCore.Mvc.Formatters.ProtoBuf.Swagger
{
    /// <summary>
    ///     ApplicationBuilderExtensions
    /// </summary>
    public static class ApplicationBuilderExtensions
    {
        /// <summary>
        ///     UseSwagger
        /// </summary>
        public static IApplicationBuilder UseSwaggerWithApiVersionDescriptionProvider(
            this IApplicationBuilder builder)
        {
            var apiVersionDescriptionProvider = builder.ApplicationServices.GetService<IApiVersionDescriptionProvider>();
            builder.UseSwagger()
                   .UseSwaggerUI(options =>
                   {
                       foreach (var apiVersionDescription in apiVersionDescriptionProvider.ApiVersionDescriptions)
                           options.SwaggerEndpoint($"/swagger/{apiVersionDescription.GroupName}/swagger.json", apiVersionDescription.GroupName.ToUpperInvariant());

                       options.DisplayRequestDuration();
                       options.DefaultModelRendering(ModelRendering.Model);
                       options.DefaultModelExpandDepth(3);
                   });

            return builder;
        }
    }
}