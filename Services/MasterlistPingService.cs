using altV_FakeMasterlist.Clients;
using altV_FakeMasterlist.Models;
using altV_FakeMasterlist.Utilities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace altV_FakeMasterlist.Services
{
    public sealed class MasterlistPingService : IHostedService
    {
        private readonly Logger _logger;
        private readonly AltHttpRelayClient _altRelayClient;
        private readonly AltHttpApiClient _altApiClient;
        private readonly IConfiguration _config;

        private Timer _pingTimer;

        public MasterlistPingService(IServiceProvider services, Logger logger, AltHttpRelayClient altRelayClient, AltHttpApiClient altApiClient)
        {
            _logger = logger;
            _altRelayClient = altRelayClient;
            _altApiClient = altApiClient;

            _config = services.GetRequiredService<IConfiguration>();
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.Console(ConsoleLogType.SUCCESS, "Masterlist-Ping", "Startup service...");
            _pingTimer = new Timer(Ping, null, TimeSpan.Zero, TimeSpan.FromSeconds(2));

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _pingTimer?.Dispose();
            _logger.Console(ConsoleLogType.SUCCESS, "Masterlist-Ping", "Stopped service...");

            return Task.CompletedTask;
        }

        private async void Ping(object state)
        {
            int players_count = Convert.ToInt32(_config["PLAYERS"]);

            /*
             * Use this to copy players count of another servers.
             * Get Server ID: http://api.altv.mp/servers/list
                var fetchRequest = new HttpRequestMessage(HttpMethod.Post, "/server/ed3a0f73e15e5ab6927a470dfe73f582");
                HttpResponseMessage fetchResponse = await _altApiClient.Client.SendAsync(fetchRequest);

                AltServerModel clonedServer = JsonConvert.DeserializeObject<AltServerModel>(await fetchResponse.Content.ReadAsStringAsync());
                if (clonedServer == null) return;

                players_count = clonedServer.info.players += 10;
            */

            var pingRequest = new HttpRequestMessage(HttpMethod.Post, "/server/ping");

            var pingRequestContent = string.Format("players-count={0}", Uri.EscapeDataString(players_count.ToString()));
            pingRequest.Content = new StringContent(pingRequestContent, Encoding.UTF8, "application/x-www-form-urlencoded");

            HttpResponseMessage pingResponse = await _altRelayClient.Client.SendAsync(pingRequest);

            _logger.Console(ConsoleLogType.SUCCESS, "Masterlist-Ping", $"Ping... | {pingRequestContent.Split("=")[1]} Players - {await pingResponse.Content.ReadAsStringAsync()}");
        }
    }
}
