﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace SlackAPI.Web_API.Auth
{
    public partial class RootObject : BaseReturn
    {
        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("team")]
        public string Team { get; set; }

        [JsonProperty("user")]
        public string User { get; set; }

        [JsonProperty("team_id")]
        public string TeamId { get; set; }

        [JsonProperty("user_id")]
        public string UserId { get; set; }
    }
}
