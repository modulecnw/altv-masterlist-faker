using AltV.Net;
using AltV.Net.Async;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading;

namespace altV_FakeMasterlist
{
    public class Resource : AsyncResource
    {
        private readonly Startup _startup;
        private readonly IServiceProvider _serviceProvider;

        public Resource()
        {
            var services = new ServiceCollection();
            _startup = new Startup();
            _startup.InitializeServices(services);

            _serviceProvider = services.BuildServiceProvider();
        }

        public override void OnStart()
        {
            foreach(var service in _serviceProvider.GetServices<IHostedService>())
            {
                service.StartAsync(CancellationToken.None);
            }
        }

        public override void OnStop()
        {
            foreach (var service in _serviceProvider.GetServices<IHostedService>())
            {
                service.StopAsync(CancellationToken.None);
            }
        }
    }
}
