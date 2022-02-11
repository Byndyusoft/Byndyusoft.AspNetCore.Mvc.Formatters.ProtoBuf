using System.Net.Http.ProtoBuf;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Formatters;
using ProtoBuf.Meta;

namespace Microsoft.AspNetCore.Mvc
{
    /// <summary>
    ///     An action result which formats the given object as ProtoBuf.
    /// </summary>
    public class ProtoBufResult : ObjectResult
    {
        /// <summary>
        ///     Creates a new <see cref="ProtoBufResult" /> with the given <paramref name="value" />.
        /// </summary>
        /// <param name="value">The value to format as ProtoBuf.</param>
        public ProtoBufResult(object? value)
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
        public ProtoBufResult(object? value, TypeModel typeModel)
            : base(value)
        {
            var formatter = new ProtoBufOutputFormatter(typeModel);

            Formatters.Add(formatter);
            ContentTypes = formatter.SupportedMediaTypes;
            TypeModel = Guard.NotNull(typeModel, nameof(typeModel));
        }

        /// <summary>
        ///     Gets or sets the <see cref="TypeModel" />.
        /// </summary>
        public TypeModel TypeModel { get; set; }

        /// <inheritdoc />
        public override Task ExecuteResultAsync(ActionContext context)
        {
            Guard.NotNull(context, nameof(context));

            return base.ExecuteResultAsync(context);
        }
    }
}