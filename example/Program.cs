using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace Byndyusoft.AspNetCore.Mvc.Formatters.ProtoBuf
{
    /// <summary>
    ///     Program
    /// </summary>
    public static class Program
    {
        /// <summary>
        ///     Main
        /// </summary>
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        private static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                       .ConfigureWebHostDefaults(webBuilder =>
                       {
                           webBuilder.UseStartup<Startup>();
                       });
        }
    }
}