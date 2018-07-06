using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace SlackAPI.Chat
{
    public class ChatInfo : BaseReturn
    {
        [JsonProperty("channel")]
        public string Channel { get; set; }

        [JsonProperty("ts")]
        public string Ts { get; set; }
    }
}
