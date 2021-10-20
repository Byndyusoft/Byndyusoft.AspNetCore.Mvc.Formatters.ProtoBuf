// ReSharper disable CheckNamespace

using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    ///     Extension methods for adding ProtoBuf formatters to MVC.
    /// </summary>
    public static class ProtoBufMvcBuilderExtensions
    {
        /// <summary>
        ///     Adds the ProtoBuf formatters to MVC.
        /// </summary>
        /// <param name="builder">The <see cref="IMvcBuilder" />.</param>
        /// <returns>The <see cref="IMvcBuilder" />.</returns>
        public static IMvcBuilder AddProtoBufFormatters(this IMvcBuilder builder)
        {
            if (builder == null) throw new ArgumentNullException(nameof(builder));

            AddProtoBufFormatterServices(builder.Services);
            return builder;
        }

        /// <summary>
        ///     Adds the ProtoBuf formatters to MVC.
        /// </summary>
        /// <param name="builder">The <see cref="IMvcBuilder" />.</param>
        /// <param name="setupAction">The <see cref="MvcProtoBufOptions" /> which need to be configured.</param>
        /// <returns>The <see cref="IMvcBuilder" />.</returns>
        public static IMvcBuilder AddProtoBufFormatters(this IMvcBuilder builder,
            Action<MvcProtoBufOptions> setupAction)
        {
            if (builder == null) throw new ArgumentNullException(nameof(builder));
            if (setupAction == null) throw new ArgumentNullException(nameof(setupAction));

            builder.Services.Configure(setupAction);

            return AddProtoBufFormatters(builder);
        }

        /// <summary>
        ///     Adds the ProtoBuf formatters to MVC.
        /// </summary>
        /// <param name="builder">The <see cref="IMvcCoreBuilder" />.</param>
        /// <returns>The <see cref="IMvcCoreBuilder" />.</returns>
        public static IMvcCoreBuilder AddProtoBufFormatters(this IMvcCoreBuilder builder)
        {
            if (builder == null) throw new ArgumentNullException(nameof(builder));

            AddProtoBufFormatterServices(builder.Services);
            return builder;
        }

        /// <summary>
        ///     Adds the ProtoBuf formatters to MVC.
        /// </summary>
        /// <param name="builder">The <see cref="IMvcCoreBuilder" />.</param>
        /// <param name="setupAction">The <see cref="MvcProtoBufOptions" /> which need to be configured.</param>
        /// <returns>The <see cref="IMvcCoreBuilder" />.</returns>
        public static IMvcCoreBuilder AddProtoBufFormatters(this IMvcCoreBuilder builder,
            Action<MvcProtoBufOptions> setupAction)
        {
            if (builder == null) throw new ArgumentNullException(nameof(builder));
            if (setupAction == null) throw new ArgumentNullException(nameof(setupAction));

            builder.Services.Configure(setupAction);

            return AddProtoBufFormatters(builder);
        }

        private static void AddProtoBufFormatterServices(IServiceCollection services)
        {
            services.TryAddEnumerable(
                ServiceDescriptor.Transient<IConfigureOptions<MvcOptions>, MvcProtoBufOptionsSetup>());
            services.TryAddEnumerable(
                ServiceDescriptor
                    .Transient<IPostConfigureOptions<MvcProtoBufOptions>,
                        MvcProtoBufOptionsConfigureCompatibilityOptions>());
        }
    }
}