using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace SlackAPI.Users.Profile
{
    public partial class RootObject : BaseReturn
    {
        [JsonProperty("profile")]
        public Dictionary<string, string> Profile { get; set; }
    }
}
