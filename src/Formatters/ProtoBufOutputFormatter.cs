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
            Guard.NotNull(options, nameof(options));
            
            TypeModel = options.TypeModel;

            foreach (var mediaType in options.SupportedMediaTypes) SupportedMediaTypes.Add(mediaType);
        }

        internal ProtoBufOutputFormatter(TypeModel typeModel)
        {
            TypeModel = Guard.NotNull(typeModel, nameof(typeModel));

            SupportedMediaTypes.Add(ProtoBufDefaults.MediaTypes.ApplicationProtoBuf);
            SupportedMediaTypes.Add(ProtoBufDefaults.MediaTypes.ApplicationXProtoBuf);
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
        protected override bool CanWriteType(Type? type)
        {
            return type == null || base.CanWriteType(type) && !type.IsAbstract && !type.IsInterface && TypeModel.CanSerialize(type);
        }

        /// <inheritdoc />
        public override async Task WriteResponseBodyAsync(OutputFormatterWriteContext context)
        {
            Guard.NotNull(context, nameof(context));

            await using var content = ProtoBufContent.Create(context.ObjectType ?? typeof(string), context.Object, TypeModel);
            await content.CopyToAsync(context.HttpContext.Response.Body).ConfigureAwait(false);
            context.HttpContext.Response.Headers.ContentLength = content.Headers.ContentLength;
        }
    }
}