using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace SlackAPI.Conversations.List
{
    public partial class RootObject : BaseReturn
    {
        [JsonProperty("channels")]
        public List<Conversation> Channels { get; set; }

        [JsonProperty("response_metadata")]
        public ResponseMetadata ResponseMetadata { get; set; }
    }
}
