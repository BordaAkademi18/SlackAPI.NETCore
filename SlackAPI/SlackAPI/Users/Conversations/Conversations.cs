using Newtonsoft.Json;
using SlackAPI.Conversations;
using System;
using System.Collections.Generic;
using System.Text;

namespace SlackAPI.Users.Conversations
{
    public partial class RootObject : BaseReturn
    {
        [JsonProperty("channels")]
        public List<Conversation> Channels { get; set; }

        [JsonProperty("response_metadata")]
        public ResponseMetadata ResponseMetadata { get; set; }
    }
}
