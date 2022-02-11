using System.Linq;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Microsoft.AspNetCore.Mvc.DependencyInjection
{
    /// <summary>
    ///     A <see cref="IConfigureOptions{TOptions}" /> implementation which will add the
    ///     ProtoBuf serializer formatters to <see cref="MvcOptions" />.
    /// </summary>
    internal class MvcProtoBufOptionsSetup : IConfigureOptions<MvcOptions>
    {
        private readonly ILoggerFactory _loggerFactory;
        private readonly MvcProtoBufOptions _options;

        /// <summary>
        ///     Initializes a new instance of <see cref="ProtoBufInputFormatter" />.
        /// </summary>
        /// <param name="loggerFactory">The <see cref="ILoggerFactory" />.</param>
        /// <param name="options">The <see cref="MvcProtoBufOptions" />.</param>
        public MvcProtoBufOptionsSetup(IOptions<MvcProtoBufOptions> options, ILoggerFactory loggerFactory)
        {
            _loggerFactory = loggerFactory;
            _options = options.Value;
        }

        /// <inheritdoc />
        public void Configure(MvcOptions options)
        {
            ConfigureFormatters(options);
            ConfigureMediaTypeFormat(options);
        }

        private void ConfigureMediaTypeFormat(MvcOptions options)
        {
            var mapping = options.FormatterMappings.GetMediaTypeMappingForFormat(_options.MediaTypeFormat);
            if (!string.IsNullOrEmpty(mapping)) return;

            var mediaType = _options.SupportedMediaTypes.FirstOrDefault();
            if (mediaType == null) return;

            options.FormatterMappings.SetMediaTypeMappingForFormat(_options.MediaTypeFormat, mediaType);
        }

        private void ConfigureFormatters(MvcOptions options)
        {
            options.OutputFormatters.Add(new ProtoBufOutputFormatter(_options));
            options.InputFormatters.Add(new ProtoBufInputFormatter(_options,
                _loggerFactory.CreateLogger<ProtoBufInputFormatter>()));
        }
    }
}