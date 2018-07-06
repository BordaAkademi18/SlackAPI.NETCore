using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace SlackAPI.Conversations.Open
{
    public partial class RootObject : Close.RootObject
    {
        [JsonProperty("channel")]
        public Conversation Channel { get; set; }
    }
}
