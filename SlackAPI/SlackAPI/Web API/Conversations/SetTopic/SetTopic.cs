using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace SlackAPI.Conversations.SetTopic
{
    public partial class RootObject : BaseReturn
    {
        [JsonProperty("topic")]
        public string Topic { get; set; }
    }
}
