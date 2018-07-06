using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace SlackAPI
{
    public partial class ResponseMetadata
    {
        [JsonProperty("next_cursor")]
        public string NextCursor { get; set; }

        [JsonProperty("warnings")]
        public List<string> Warnings { get; set; }
    }
}
