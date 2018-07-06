using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace SlackAPI.Team.Profile
{
    public partial class RootObject : BaseReturn
    {
        [JsonProperty("profile")]
        public Profile Profile { get; set; }
    }

    public partial class Profile
    {
        [JsonProperty("fields")]
        public List<Field> Fields { get; set; }
    }

    public partial class Field
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("ordering")]
        public long Ordering { get; set; }

        [JsonProperty("label")]
        public string Label { get; set; }

        [JsonProperty("hint")]
        public string Hint { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("possible_values")]
        public List<string> PossibleValues { get; set; }

        [JsonProperty("options")]
        public Options Options { get; set; }

        [JsonProperty("is_hidden", NullValueHandling = NullValueHandling.Ignore)]
        public long? IsHidden { get; set; }
    }

    public partial class Options
    {
        [JsonProperty("is_protected")]
        public long IsProtected { get; set; }
    }
}
