using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace SlackAPI
{
    public class BaseReturn
    {
        [JsonProperty("ok")]
        public bool Ok { get; set; }
    }
}
