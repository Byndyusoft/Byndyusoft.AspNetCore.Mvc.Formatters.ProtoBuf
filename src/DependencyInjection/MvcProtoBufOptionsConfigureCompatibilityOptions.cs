// ReSharper disable CheckNamespace

using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Microsoft.Extensions.DependencyInjection
{
    internal class
        MvcProtoBufOptionsConfigureCompatibilityOptions : ConfigureCompatibilityOptions<MvcProtoBufOptions>
    {
        public MvcProtoBufOptionsConfigureCompatibilityOptions(
            ILoggerFactory loggerFactory,
            IOptions<MvcCompatibilityOptions> compatibilityOptions)
            : base(loggerFactory, compatibilityOptions)
        {
        }

        protected override IReadOnlyDictionary<string, object> DefaultValues
        {
            get
            {
                var values = new Dictionary<string, object>();

                if (Version >= CompatibilityVersion.Version_2_1)
                    values[nameof(MvcProtoBufOptions.AllowInputFormatterExceptionMessages)] = true;

                return values;
            }
        }
    }
}