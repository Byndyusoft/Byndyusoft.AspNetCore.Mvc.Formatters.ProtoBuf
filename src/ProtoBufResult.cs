using System;
using System.Net.Http.ProtoBuf;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Net.Http.Headers;
using ProtoBuf.Meta;

namespace Microsoft.AspNetCore.Mvc
{
    /// <summary>
    ///     An action result which formats the given object as ProtoBuf.
    /// </summary>
    public class ProtoBufResult : ActionResult, IStatusCodeActionResult
    {
        /// <summary>
        ///     Creates a new <see cref="ProtoBufResult" /> with the given <paramref name="value" />.
        /// </summary>
        /// <param name="value">The value to format as ProtoBuf.</param>
        public ProtoBufResult(object value)
            : this(value, ProtoBufDefaults.TypeModel)
        {
        }

        /// <summary>
        ///     Creates a new <see cref="ProtoBufResult" /> with the given <paramref name="value" />.
        /// </summary>
        /// <param name="value">The value to format as ProtoBuf.</param>
        /// <param name="typeModel">
        ///     The <see cref="TypeModel" /> to be used by the formatter.
        /// </param>
        public ProtoBufResult(object value, TypeModel typeModel)
        {
            Value = value;
            TypeModel = typeModel ?? throw new ArgumentNullException(nameof(typeModel));
        }

        /// <summary>
        ///     Gets or sets the <see cref="MediaTypeHeaderValue" /> representing the Content-Type header of the response.
        /// </summary>
        public string ContentType { get; set; } = ProtoBufDefaults.MediaType;

        /// <summary>
        ///     Gets or sets the <see cref="TypeModel" />.
        /// </summary>
        public TypeModel TypeModel { get; set; }

        /// <summary>
        ///     Gets or sets the value to be formatted.
        /// </summary>
        public object Value { get; set; }

        /// <summary>
        ///     Gets or sets the HTTP status code.
        /// </summary>
        public int? StatusCode { get; set; }

        /// <inheritdoc />
        public override async Task ExecuteResultAsync(ActionContext context)
        {
            if (context == null) throw new ArgumentNullException(nameof(context));

            context.HttpContext.Response.ContentType = ContentType;

            using var content = ProtoBufContent.Create(Value, Value?.GetType() ?? typeof(object), TypeModel);
            await content.CopyToAsync(context.HttpContext.Response.Body)
                .ConfigureAwait(false);
        }
    }
}