using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace SlackAPI.Files.CommentResult
{
    public partial class CommentResult : BaseReturn
    {
        [JsonProperty("comment")]
        public Comment Comment { get; set; }
    }
}
