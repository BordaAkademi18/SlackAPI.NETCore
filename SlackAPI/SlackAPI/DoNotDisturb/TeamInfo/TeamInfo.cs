using Newtonsoft.Json;
using SlackAPI.DoNotDisturb.Info;
using System;
using System.Collections.Generic;
using System.Text;

namespace SlackAPI.DoNotDisturb.TeamInfo
{
    public partial class RootObject : BaseReturn
    {
        [JsonProperty("users")]
        public Dictionary<string, DndInfo> Users { get; set; }
    }   
}
