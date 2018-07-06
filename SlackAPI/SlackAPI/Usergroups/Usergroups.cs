using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace SlackAPI.Usergroups
{
    public partial class Usergroup
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("team_id")]
        public string TeamId { get; set; }

        [JsonProperty("is_usergroup")]
        public bool IsUsergroup { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("handle")]
        public string Handle { get; set; }

        [JsonProperty("is_external")]
        public bool IsExternal { get; set; }

        [JsonProperty("date_create")]
        public long DateCreate { get; set; }

        [JsonProperty("date_update")]
        public long DateUpdate { get; set; }

        [JsonProperty("date_delete")]
        public long DateDelete { get; set; }

        [JsonProperty("auto_type")]
        public string AutoType { get; set; }

        [JsonProperty("created_by")]
        public string CreatedBy { get; set; }

        [JsonProperty("updated_by")]
        public string UpdatedBy { get; set; }

        [JsonProperty("deleted_by")]
        public object DeletedBy { get; set; }

        [JsonProperty("prefs")]
        public Prefs Prefs { get; set; }

        [JsonProperty("user_count")]
        public string UserCount { get; set; }
    }

    public partial class Prefs
    {
        [JsonProperty("channels")]
        public List<string> Channels { get; set; }

        [JsonProperty("groups")]
        public List<string> Groups { get; set; }
    }
}
