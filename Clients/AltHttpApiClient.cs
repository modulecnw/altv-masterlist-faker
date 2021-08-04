using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace altV_FakeMasterlist.Clients
{
    public sealed class AltHttpApiClient
    {
        public AltHttpApiClient(HttpClient client)
        {
            Client = client;
        }

        public HttpClient Client { get; }
    }
}
