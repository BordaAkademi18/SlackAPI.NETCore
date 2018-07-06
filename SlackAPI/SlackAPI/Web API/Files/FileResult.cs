using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace SlackAPI.Files
{
    public partial class FileResult : BaseReturn
    {
        [JsonProperty("file")]
        public File File { get; set; }
    }
}
