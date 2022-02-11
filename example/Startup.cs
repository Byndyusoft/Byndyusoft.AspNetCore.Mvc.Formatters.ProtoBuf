using Byndyusoft.AspNetCore.Mvc.Formatters.ProtoBuf.Swagger;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Byndyusoft.AspNetCore.Mvc.Formatters.ProtoBuf
{
    /// <summary>
    ///     Startup
    /// </summary>
    public class Startup
    {
        /// <summary>
        ///     ConfigureServices
        /// </summary>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddApiVersioning(options =>
                {
                    options.DefaultApiVersion = ApiVersion.Default;
                    options.AssumeDefaultVersionWhenUnspecified = true;
                    options.ReportApiVersions = true;
                })
                .AddVersionedApiExplorer(options =>
                {
                    options.GroupNameFormat = "'v'VVV";
                    options.SubstituteApiVersionInUrl = true;
                });

            services.AddSwagger();

            services.AddMvcCore()
                .AddProtoBufFormatters()
                .AddFormatterMappings();
            services.AddControllers();
        }

        /// <summary>
        /// Configure
        /// </summary>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IApiVersionDescriptionProvider apiVersionDescriptionProvider)
        {
            if (env.IsProduction() == false)
                app.UseSwagger(apiVersionDescriptionProvider);

            app
                .UseRouting()
                .UseEndpoints(endpoints => endpoints.MapControllers());
        }
    }
}