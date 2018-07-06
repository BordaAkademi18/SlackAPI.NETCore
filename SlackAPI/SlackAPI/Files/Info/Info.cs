using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace SlackAPI.Files.Info
{
    public partial class RootObject : BaseReturn
    {
        [JsonProperty("file")]
        public File File { get; set; }

        [JsonProperty("comments")]
        public List<Comment> Comments { get; set; }

        [JsonProperty("paging")]
        public Paging Paging { get; set; }

    }

    public partial class Paging
    {
        [JsonProperty("count")]
        public long Count { get; set; }

        [JsonProperty("total")]
        public long Total { get; set; }

        [JsonProperty("page")]
        public long Page { get; set; }

        [JsonProperty("pages")]
        public long Pages { get; set; }
    }
}
