using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace SlackAPI
{
    public class BaseError : BaseReturn
    {
        [JsonProperty("error")]
        public string Error { get; set; }

        [JsonProperty("detail")]
        public string Detail { get; set; }

        [JsonProperty("needed")]
        public string Needed { get; set; }

        [JsonProperty("provided")]
        public string Provided { get; set; }
    }
}
