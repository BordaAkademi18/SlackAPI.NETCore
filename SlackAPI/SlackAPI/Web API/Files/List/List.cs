using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace SlackAPI.Files.List
{
    public partial class RootObject : BaseReturn
    {
        [JsonProperty("files")]
        public List<File> Files { get; set; }

        [JsonProperty("paging")]
        public Paging Paging { get; set; }

    }
}
