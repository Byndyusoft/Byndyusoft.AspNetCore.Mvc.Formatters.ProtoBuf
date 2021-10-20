using System.Collections;
using System.Collections.Generic;
using System.Net.Http.ProtoBuf;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using ProtoBuf.Meta;

namespace Microsoft.AspNetCore.Mvc
{
    /// <summary>
    ///     Provides configuration for ProtoBuf formatters.
    /// </summary>
    public class MvcProtoBufOptions : IEnumerable<ICompatibilitySwitch>
    {
        private readonly CompatibilitySwitch<bool> _allowInputFormatterExceptionMessages;

        /// <summary>
        ///     Initializes a new instance of <see cref="MvcProtoBufOptions" />.
        /// </summary>
        public MvcProtoBufOptions()
        {
            _allowInputFormatterExceptionMessages =
                new CompatibilitySwitch<bool>(nameof(AllowInputFormatterExceptionMessages));

            SupportedMediaTypes.Add(ProtoBufDefaults.MediaTypes.ApplicationProtoBuf);
            SupportedMediaTypes.Add(ProtoBufDefaults.MediaTypes.ApplicationXProtoBuf);
        }

        /// <summary>
        ///     Gets or sets a flag to determine whether error messages from ProtoBuf deserialization by the
        ///     <see cref="ProtoBufInputFormatter" /> will be added to the <see cref="ModelStateDictionary" />. The default
        ///     value is <c>false</c>, meaning that a generic error message will be used instead.
        /// </summary>
        /// <remarks>
        ///     <para>
        ///         Error messages in the <see cref="ModelStateDictionary" /> are often communicated to clients, either in HTML
        ///         or using <see cref="BadRequestObjectResult" />. In effect, this setting controls whether clients can receive
        ///         detailed error messages about submitted ProtoBuf data.
        ///     </para>
        ///     <para>
        ///         This property is associated with a compatibility switch and can provide a different behavior depending on
        ///         the configured compatibility version for the application. See <see cref="CompatibilityVersion" /> for
        ///         guidance and examples of setting the application's compatibility version.
        ///     </para>
        ///     <para>
        ///         Configuring the desired of the value compatibility switch by calling this property's setter will take
        ///         precedence
        ///         over the value implied by the application's <see cref="CompatibilityVersion" />.
        ///     </para>
        ///     <para>
        ///         If the application's compatibility version is set to <see cref="CompatibilityVersion.Version_2_0" /> then
        ///         this setting will have value <c>false</c> unless explicitly configured.
        ///     </para>
        ///     <para>
        ///         If the application's compatibility version is set to <see cref="CompatibilityVersion.Version_2_1" /> or
        ///         higher then this setting will have value <c>true</c> unless explicitly configured.
        ///     </para>
        /// </remarks>
        public bool AllowInputFormatterExceptionMessages
        {
            get => _allowInputFormatterExceptionMessages.Value;
            set => _allowInputFormatterExceptionMessages.Value = value;
        }

        /// <summary>
        ///     Gets the mutable collection of media type elements supported by ProtoBufFormatters.
        /// </summary>
        public HashSet<string> SupportedMediaTypes { get; } = new HashSet<string>();

        /// <summary>
        ///     The format value.
        /// </summary>
        public string MediaTypeFormat { get; set; } = ProtoBufDefaults.MediaTypeFormat;

        /// <summary>
        ///     Gets and sets the <see cref="ProtoBuf.Meta.TypeModel" /> that are used by this application.
        /// </summary>
        public TypeModel TypeModel { get; set; } = ProtoBufDefaults.TypeModel;

        /// <inheritdoc />
        public IEnumerator<ICompatibilitySwitch> GetEnumerator()
        {
            yield return _allowInputFormatterExceptionMessages;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}