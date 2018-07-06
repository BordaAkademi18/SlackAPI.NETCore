using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace SlackAPI.Usergroups.List
{
    public partial class RootObject : BaseReturn
    {
        [JsonProperty("usergroups")]
        public List<Usergroup> Usergroups { get; set; }
    }
}
