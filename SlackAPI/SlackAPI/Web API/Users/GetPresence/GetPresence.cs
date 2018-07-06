using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace SlackAPI.Users.GetPresence
{
    public partial class RootObject : BaseReturn
    {
        [JsonProperty("presence")]
        public string Presence { get; set; }

        [JsonProperty("online")]
        public bool Online { get; set; }

        [JsonProperty("auto_away")]
        public bool AutoAway { get; set; }

        [JsonProperty("manual_away")]
        public bool ManualAway { get; set; }

        [JsonProperty("connection_count")]
        public long ConnectionCount { get; set; }

        [JsonProperty("last_activity")]
        public long LastActivity { get; set; }
    }
}
