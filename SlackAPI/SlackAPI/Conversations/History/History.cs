using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace SlackAPI.Conversations.History
{
    public partial class RootObject : BaseReturn
    {
        [JsonProperty("latest")]
        public string Latest { get; set; }

        [JsonProperty("messages")]
        public List<Message> Messages { get; set; }

        [JsonProperty("has_more")]
        public bool HasMore { get; set; }

        [JsonProperty("pin_count")]
        public long PinCount { get; set; }

        [JsonProperty("response_metadata")]
        public ResponseMetadata ResponseMetadata { get; set; }
    }
}
