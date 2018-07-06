using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace SlackAPI.Conversations.SetPurpose
{
    public partial class RootObject : BaseReturn
    {
        [JsonProperty("purpose")]
        public string Purpose { get; set; }
    }
}
