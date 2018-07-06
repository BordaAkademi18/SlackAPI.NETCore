using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace SlackAPI.Conversations.Close
{
    public partial class RootObject : BaseReturn
    {
        [JsonProperty("no_op")]
        public bool NoOp { get; set; }

        [JsonProperty("already_closed")]
        public bool AlreadyClosed { get; set; }
    }
}
