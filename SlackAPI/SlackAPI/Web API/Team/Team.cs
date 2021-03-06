﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace SlackAPI.Team
{
    public partial class Team
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("domain")]
        public string Domain { get; set; }

        [JsonProperty("email_domain")]
        public string EmailDomain { get; set; }

        [JsonProperty("icon")]
        public Icon Icon { get; set; }

        [JsonProperty("enterprise_id")]
        public string EnterpriseId { get; set; }

        [JsonProperty("enterprise_name")]
        public string EnterpriseName { get; set; }
    }

    public partial class Icon
    {
        [JsonProperty("image_34")]
        public string Image34 { get; set; }

        [JsonProperty("image_44")]
        public string Image44 { get; set; }

        [JsonProperty("image_68")]
        public string Image68 { get; set; }

        [JsonProperty("image_88")]
        public string Image88 { get; set; }

        [JsonProperty("image_102")]
        public string Image102 { get; set; }

        [JsonProperty("image_132")]
        public string Image132 { get; set; }

        [JsonProperty("image_default")]
        public bool ImageDefault { get; set; }
    }
}
