using System;
using System.Net;
using System.Net.Http;
using System.Net.Sockets;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Byndyusoft.AspNetCore.Mvc.Formatters.Functional
{
    public abstract class MvcTestFixture : IDisposable
    {
        private readonly string _url = $"http://localhost:{FreeTcpPort()}";
        private HttpClient? _client;
        private IHost? _host;

        protected MvcTestFixture()
        {
            _host = Host
                .CreateDefaultBuilder()
                .ConfigureWebHostDefaults(
                    webBuilder =>
                    {
                        webBuilder.UseUrls(_url);
                        webBuilder.ConfigureServices(ConfigureServices);
                        webBuilder.Configure(Configure);
                    }
                )
                .Build();
            _host.Start();
        }

        protected HttpClient Client
        {
            get
            {
                if (_client == null)
                {
                    _client = new HttpClient
                    {
                        BaseAddress = new Uri(_url),
                        DefaultRequestVersion = new Version(1, 0)
                    };
                    ConfigureHttpClient(_client);
                }

                return _client;
            }
        }

        public virtual void Dispose()
        {
            _host?.Dispose();
            _host = null;

            _client?.Dispose();
            _client = null;
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseDeveloperExceptionPage();
            app.UseRouting();
            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddLogging(c => c.ClearProviders());
            services.AddControllers();
            ConfigureMvc(services.AddMvcCore());
        }

        protected abstract void ConfigureMvc(IMvcCoreBuilder builder);

        protected virtual void ConfigureHttpClient(HttpClient client)
        {
        }

        private static int FreeTcpPort()
        {
            var listener = new TcpListener(IPAddress.Loopback, 0);
            listener.Start();
            var port = ((IPEndPoint) listener.LocalEndpoint).Port;
            listener.Stop();
            return port;
        }
    }
}