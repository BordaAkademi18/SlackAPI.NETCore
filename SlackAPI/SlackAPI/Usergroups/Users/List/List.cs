using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace SlackAPI.Usergroups.Users.List
{
    public partial class RootObject : BaseReturn
    {
        [JsonProperty("users")]
        public List<string> Users { get; set; }
    }
}
