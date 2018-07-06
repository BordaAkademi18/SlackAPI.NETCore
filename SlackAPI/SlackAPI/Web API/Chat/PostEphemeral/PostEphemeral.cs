using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace SlackAPI.Chat.PostEphemeral
{
    public class RootObject : BaseReturn
    {
        [JsonProperty("message_ts")]
        public string MessageTs { get; set; }
    }
}
