using System;
using System.Net.Http;
using System.Net.Http.ProtoBuf;
using System.Threading.Tasks;
using ProtoBuf;
using Microsoft.Extensions.Logging;
using ProtoBuf.Meta;

namespace Microsoft.AspNetCore.Mvc.Formatters
{
    /// <summary>
    ///     A <see cref="InputFormatter" /> for ProtoBuf content that uses ProtoBuf serialization.
    /// </summary>
    public class ProtoBufInputFormatter : InputFormatter, IInputFormatterExceptionPolicy
    {
        private readonly ILogger<ProtoBufInputFormatter> _logger;

        /// <summary>
        ///     Initializes a new instance of <see cref="ProtoBufInputFormatter" />.
        /// </summary>
        /// <param name="logger">The <see cref="ILogger" />.</param>
        /// <param name="options">The <see cref="MvcProtoBufOptions" />.</param>
        public ProtoBufInputFormatter(MvcProtoBufOptions options, ILogger<ProtoBufInputFormatter> logger)
        {
            if (options == null) throw new ArgumentNullException(nameof(options));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));

            TypeModel = options.TypeModel;

            foreach (var mediaType in options.SupportedMediaTypes) SupportedMediaTypes.Add(mediaType);
        }

        /// <summary>
        ///     Gets the <see cref="ProtoBuf.Meta.TypeModel" /> used to configure the ProtoBuf serialization.
        /// </summary>
        /// <remarks>
        ///     A single instance of <see cref="ProtoBufInputFormatter" /> is used for all ProtoBuf formatting. Any
        ///     changes to the options will affect all input formatting.
        /// </remarks>
        public TypeModel TypeModel { get; }

        /// <inheritdoc />
        InputFormatterExceptionPolicy IInputFormatterExceptionPolicy.ExceptionPolicy =>
            InputFormatterExceptionPolicy.MalformedInputExceptions;

        /// <inheritdoc />
        protected override bool CanReadType(Type type)
        {
            return base.CanReadType(type) && !type.IsAbstract && !type.IsInterface;
        }

        /// <inheritdoc />
        public override async Task<InputFormatterResult> ReadRequestBodyAsync(InputFormatterContext context)
        {
            if (context == null) throw new ArgumentNullException(nameof(context));

            object model;

            try
            {
                using var content = new StreamContent(context.HttpContext.Request.Body);
                model = await content.ReadFromProtoBufAsync(context.ModelType, TypeModel)
                    .ConfigureAwait(false);
            }
            catch (ProtoException exception)
            {
                Log.ProtoBufInputException(_logger, exception);
                context.ModelState.TryAddModelError(string.Empty, exception, context.Metadata);
                return await InputFormatterResult.FailureAsync();
            }

            if (model == null && context.TreatEmptyInputAsDefaultValue == false)
                return await InputFormatterResult.NoValueAsync();

            Log.ProtoBufInputSuccess(_logger, context.ModelType);
            return await InputFormatterResult.SuccessAsync(model);
        }

        private static class Log
        {
            // ReSharper disable InconsistentNaming
            private static readonly Action<ILogger, string, Exception> _msgpackInputFormatterException;

            private static readonly Action<ILogger, string, Exception> _msgpackInputSuccess;
            // ReSharper enable InconsistentNaming

            static Log()
            {
                _msgpackInputFormatterException = LoggerMessage.Define<string>(
                    LogLevel.Debug,
                    new EventId(1, "ProtoBufInputException"),
                    "ProtoBuf input formatter threw an exception: {Message}");
                _msgpackInputSuccess = LoggerMessage.Define<string>(
                    LogLevel.Debug,
                    new EventId(2, "ProtoBufInputSuccess"),
                    "ProtoBuf input formatter succeeded, deserializing to type '{TypeName}'");
            }

            public static void ProtoBufInputException(ILogger logger, Exception exception)
            {
                _msgpackInputFormatterException(logger, exception.Message, exception);
            }

            public static void ProtoBufInputSuccess(ILogger logger, Type modelType)
            {
                _msgpackInputSuccess(logger, modelType.FullName, null);
            }
        }
    }
}