using altV_FakeMasterlist.Clients;
using altV_FakeMasterlist.Services;
using altV_FakeMasterlist.Utilities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace altV_FakeMasterlist
{
    public sealed class Startup
    {
        private readonly IConfiguration _config;

        public Startup()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location))
                .AddJsonFile("config.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables();

            _config = builder.Build();
        }

        public void InitializeServices(IServiceCollection services)
        {
            services.AddSingleton<Logger>();
            services.AddSingleton(_config);

            services.AddHttpClient<AltHttpRelayClient>(client =>
            {
                client.BaseAddress = new Uri("http://relay-default.altv.mp/");
                client.DefaultRequestHeaders.Add("Master-Token", _config["MASTERLIST_TOKEN"]);
                client.DefaultRequestHeaders.Add("User-Agent", "AltPublicAgent");
            });

            services.AddHttpClient<AltHttpApiClient>(client =>
            {
                client.BaseAddress = new Uri("https://api.altv.mp/");
                client.DefaultRequestHeaders.Add("User-Agent", "AltPublicAgent");
            });

            services.AddHostedService<MasterlistPingService>();
        }
    }
}
