using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace SlackAPI.Team.Info
{
    public partial class RootObject : BaseReturn
    {
        [JsonProperty("team")]
        public Team Team { get; set; }
    }
}
