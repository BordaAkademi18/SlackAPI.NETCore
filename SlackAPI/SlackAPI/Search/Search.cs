using Newtonsoft.Json;
using SlackAPI.Conversations;
using SlackAPI.Files;
using System;
using System.Collections.Generic;
using System.Text;

namespace SlackAPI.Search
{
    public partial class RootObject : BaseReturn
    {
        [JsonProperty("files")]
        public Match<File> Files { get; set; }

        [JsonProperty("messages")]
        public Match<Message> Messages { get; set; }

        [JsonProperty("query")]
        public string Query { get; set; }
    }

    public partial class Match<T>
    {
        [JsonProperty("matches")]
        public List<T> Matches { get; set; }

        [JsonProperty("pagination")]
        public Pagination Pagination { get; set; }

        [JsonProperty("paging")]
        public Paging Paging { get; set; }

        [JsonProperty("total")]
        public long Total { get; set; }
    }

    public partial class Pagination
    {
        [JsonProperty("first")]
        public long First { get; set; }

        [JsonProperty("last")]
        public long Last { get; set; }

        [JsonProperty("page")]
        public long Page { get; set; }

        [JsonProperty("page_count")]
        public long PageCount { get; set; }

        [JsonProperty("per_page")]
        public long PerPage { get; set; }

        [JsonProperty("total_count")]
        public long TotalCount { get; set; }
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
