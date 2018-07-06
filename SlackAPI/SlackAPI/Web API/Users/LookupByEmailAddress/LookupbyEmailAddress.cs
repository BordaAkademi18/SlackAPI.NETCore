using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace SlackAPI.Users.LookupByEmailAddress
{
    public partial class RootObject : BaseReturn
    {
        [JsonProperty("user")]
        public User User { get; set; }
    }
}
