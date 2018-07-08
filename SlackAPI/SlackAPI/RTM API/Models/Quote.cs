using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace SlackAPI.RTM_API.Models
{
    public partial class RandomQuote
    {
        [JsonProperty("quote")]
        public string Quote { get; set; }

        [JsonProperty("author")]
        public string Author { get; set; }
    }
}
