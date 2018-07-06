using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace SlackAPI.Conversations.Members
{
    public partial class RootObject : BaseReturn
    {
        [JsonProperty("members")]
        public List<string> Members { get; set; }

        [JsonProperty("response_metadata")]
        public ResponseMetadata ResponseMetadata { get; set; }
    }
}
