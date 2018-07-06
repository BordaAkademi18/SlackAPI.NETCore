using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace SlackAPI.Users.List
{
    public partial class RootObject : BaseReturn
    {
        [JsonProperty("members")]
        public List<User> Members { get; set; }

        [JsonProperty("cache_ts")]
        public long CacheTs { get; set; }

        [JsonProperty("response_metadata")]
        public ResponseMetadata ResponseMetadata { get; set; }
    }
}
