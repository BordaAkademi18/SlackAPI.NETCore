using Newtonsoft.Json;
using SlackAPI.Conversations;
using System;
using System.Collections.Generic;
using System.Text;

namespace SlackAPI.Pins.List
{
    public partial class RootObject
    {
        [JsonProperty("items")]
        public List<Item> Items { get; set; }

        [JsonProperty("ok")]
        public bool Ok { get; set; }
    }

    public partial class Item
    { 
        [JsonProperty("channel")]
        public string Channel { get; set; }

        [JsonProperty("created")]
        public long Created { get; set; }

        [JsonProperty("created_by")]
        public string CreatedBy { get; set; }

        [JsonProperty("message")]
        public Message Message { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }
    }
}
