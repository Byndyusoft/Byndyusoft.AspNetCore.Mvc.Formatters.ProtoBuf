using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Options;

namespace Microsoft.AspNetCore.Mvc.DependencyInjection
{
    /// <summary>
    ///     A <see cref="IConfigureOptions{IISServerOptions}" /> implementation 
    /// </summary>
    public class IISServerOptionsSetup : IConfigureOptions<IISServerOptions>
    {
        /// <inheritdoc />
        public void Configure(IISServerOptions options)
        {
#if NETCOREAPP
            options.AllowSynchronousIO = true;
#endif
        }
    }
}