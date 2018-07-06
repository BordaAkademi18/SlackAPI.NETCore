using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace SlackAPI.Chat.Update
{
    public class RootObject : ChatInfo
    {
        [JsonProperty("text")]
        public string Text { get; set; }
    }
}
