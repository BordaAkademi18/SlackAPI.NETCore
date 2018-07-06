using Newtonsoft.Json;
using SlackAPI.Conversations;
using SlackAPI.Files;
using System;
using System.Collections.Generic;
using System.Text;

namespace SlackAPI.Reactions.List
{
    public partial class RootObject : BaseReturn
    {
        [JsonProperty("items")]
        public List<Item> Items { get; set; }

        [JsonProperty("paging")]
        public Paging Paging { get; set; }
    }

    public partial class Item
    {
        [JsonProperty("type", NullValueHandling = NullValueHandling.Ignore)]
        public string Type { get; set; }

        [JsonProperty("channel", NullValueHandling = NullValueHandling.Ignore)]
        public string Channel { get; set; }

        [JsonProperty("message", NullValueHandling = NullValueHandling.Ignore)]
        public Message Message { get; set; }

        [JsonProperty("comment", NullValueHandling = NullValueHandling.Ignore)]
        public Comment Comment { get; set; }

        [JsonProperty("file", NullValueHandling = NullValueHandling.Ignore)]
        public File File { get; set; }
    }

    public partial class Paging
    {
        [JsonProperty("count")]
        public long Count { get; set; }

        [JsonProperty("page")]
        public long Page { get; set; }

        [JsonProperty("pages")]
        public long Pages { get; set; }

        [JsonProperty("total")]
        public long Total { get; set; }
    }
}
