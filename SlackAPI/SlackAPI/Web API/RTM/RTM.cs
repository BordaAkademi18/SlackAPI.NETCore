using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace SlackAPI.RTM
{
    public partial class RTM : BaseReturn
    {
        [JsonProperty("self")]
        public Self Self { get; set; }

        [JsonProperty("team")]
        public Team Team { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }
    }

    public partial class Self
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }

    public partial class Team
    {
        [JsonProperty("domain")]
        public string Domain { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }
}
