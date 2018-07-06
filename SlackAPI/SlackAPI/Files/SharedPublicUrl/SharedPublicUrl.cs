using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace SlackAPI.Files.SharedPublicUrl
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

}
