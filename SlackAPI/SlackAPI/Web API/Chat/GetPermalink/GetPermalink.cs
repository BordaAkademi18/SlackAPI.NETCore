using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace SlackAPI.Chat.GetPermalink
{
    public class RootObject : BaseReturn
    {
        [JsonProperty("channel")]
        public string Channel { get; set; }

        [JsonProperty("permalink")]
        public string Permalink { get; set; }
    }
}
