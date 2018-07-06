using Newtonsoft.Json;
using SlackAPI.Team.Info;
using System;
using System.Collections.Generic;
using System.Text;

namespace SlackAPI.Users
{
    public partial class RootObject : BaseReturn
    {
        [JsonProperty("user")]
        public UserIdentity User { get; set; }

        [JsonProperty("team")]
        public Team.Team Team { get; set; }
    }

    public partial class UserIdentity
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("image_24")]
        public string Image24 { get; set; }

        [JsonProperty("image_32")]
        public string Image32 { get; set; }

        [JsonProperty("image_48")]
        public string Image48 { get; set; }

        [JsonProperty("image_72")]
        public string Image72 { get; set; }

        [JsonProperty("image_192")]
        public string Image192 { get; set; }
    }
}
