using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Options;

namespace Microsoft.AspNetCore.Mvc.DependencyInjection
{
    /// <summary>
    ///     A <see cref="IConfigureOptions{KestrelServerOptions}" /> implementation 
    /// </summary>
    public class KestrelServerOptionsSetup : IConfigureOptions<KestrelServerOptions>
    {
        /// <inheritdoc />
        public void Configure(KestrelServerOptions options)
        {
            options.AllowSynchronousIO = true;
        }
    }
}