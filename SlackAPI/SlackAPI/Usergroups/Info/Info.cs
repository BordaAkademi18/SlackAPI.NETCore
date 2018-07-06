using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace SlackAPI.Usergroups.Info
{
    public partial class RootObject : BaseReturn
    {
        [JsonProperty("usergroup")]
        public Usergroup Usergroup { get; set; }
    }    
}
