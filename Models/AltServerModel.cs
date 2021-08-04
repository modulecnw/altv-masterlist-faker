using System;
using System.Collections.Generic;
using System.Text;

namespace altV_FakeMasterlist.Models
{
    public class AltServerModel
    {
        public bool active { get; set; }
        public AltServerInfoModel info { get; set; }
    }

    public class AltServerInfoModel
    {
        public string id { get; set; }
        public int maxPlayers { get; set; }
        public int players { get; set; }
        public string name { get; set; }
        public bool locked { get; set; }
        public string host { get; set; }
        public string port { get; set; }
        public string gameMode { get; set; }
        public string website { get; set; }
        public string language { get; set; }
        public string description { get; set; }
        public bool verified { get; set; }
        public bool promoted { get; set; }
        public bool useEarlyAuth { get; set; }
        public string earlyAuthUrl { get; set; }
        public bool useCdn { get; set; }
        public string cdnUrl { get; set; }
        public bool useVoiceChat { get; set; }
        public string[] tags { get; set; }
        public string bannerUrl { get; set; }
        public string branch { get; set; }
        public int build { get; set; }
        public string version { get; set; }
        public long lastUpdate { get; set; }
    }
}
