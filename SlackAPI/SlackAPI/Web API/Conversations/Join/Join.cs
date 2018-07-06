using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace SlackAPI.Conversations.Join
{
    public partial class RootObject : BaseReturn
    {
        [JsonProperty("warning")]
        public string Warning { get; set; }

        [JsonProperty("response_metadata")]
        public ResponseMetadata ResponseMetadata { get; set; }

        [JsonProperty("channel")]
        public Conversation Channel { get; set; }
    }
}
