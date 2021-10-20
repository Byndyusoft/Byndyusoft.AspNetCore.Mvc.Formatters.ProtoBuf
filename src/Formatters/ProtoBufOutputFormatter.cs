using System;
using System.Net.Http.ProtoBuf;
using System.Threading.Tasks;
using ProtoBuf.Meta;

namespace Microsoft.AspNetCore.Mvc.Formatters
{
    /// <summary>
    ///     A <see cref="OutputFormatter" /> for ProtoBuf content that uses ProtoBuf serialization.
    /// </summary>
    public class ProtoBufOutputFormatter : OutputFormatter
    {
        /// <summary>
        ///     Initializes a new <see cref="ProtoBufOutputFormatter" /> instance.
        /// </summary>
        /// <param name="options">The <see cref="MvcProtoBufOptions" />.</param>
        public ProtoBufOutputFormatter(MvcProtoBufOptions options)
        {
            if (options == null) throw new ArgumentNullException(nameof(options));

            TypeModel = options.TypeModel;

            foreach (var mediaType in options.SupportedMediaTypes) SupportedMediaTypes.Add(mediaType);
        }

        /// <summary>
        ///     Gets the <see cref="ProtoBuf.Meta.TypeModel" /> used to configure the ProtoBuf serialization.
        /// </summary>
        /// <remarks>
        ///     A single instance of <see cref="ProtoBufOutputFormatter" /> is used for all ProtoBuf formatting. Any
        ///     changes to the options will affect all output formatting.
        /// </remarks>
        public TypeModel TypeModel { get; }

        /// <inheritdoc />
        protected override bool CanWriteType(Type type)
        {
            return base.CanWriteType(type) && !type.IsAbstract && !type.IsInterface;
        }

        /// <inheritdoc />
        public override async Task WriteResponseBodyAsync(OutputFormatterWriteContext context)
        {
            if (context == null) throw new ArgumentNullException(nameof(context));

            using var content = ProtoBufContent.Create(context.Object, context.ObjectType, TypeModel);
            await content.CopyToAsync(context.HttpContext.Response.Body).ConfigureAwait(false);
        }
    }
}