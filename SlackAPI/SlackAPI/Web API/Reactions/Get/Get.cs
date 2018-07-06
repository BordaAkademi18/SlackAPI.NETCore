using Newtonsoft.Json;
using SlackAPI.Conversations;
using SlackAPI.Files;
using System;
using System.Collections.Generic;
using System.Text;

namespace SlackAPI.Reactions.Get
{
    public partial class RootObject : BaseReturn
    {
        [JsonProperty("message", NullValueHandling = NullValueHandling.Ignore)]
        public Message Message { get; set; }

        [JsonProperty("comment", NullValueHandling = NullValueHandling.Ignore)]
        public Comment Comment { get; set; }

        [JsonProperty("file", NullValueHandling = NullValueHandling.Ignore)]
        public File File { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }
    }
}
