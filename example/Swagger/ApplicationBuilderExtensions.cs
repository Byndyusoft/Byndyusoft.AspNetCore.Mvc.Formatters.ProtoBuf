using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
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
        public static IApplicationBuilder UseSwagger(
            this IApplicationBuilder builder,
            IApiVersionDescriptionProvider apiVersionDescriptionProvider)
        {
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