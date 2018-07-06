using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace SlackAPI.Conversations.Info
{
    public partial class RootObject : BaseReturn
    {
        [JsonProperty("channel")]
        public Conversation Channel { get; set; }
    }
}
