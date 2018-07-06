using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace SlackAPI.Reminders
{
    public partial class Reminder
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("creator")]
        public string Creator { get; set; }

        [JsonProperty("user")]
        public string User { get; set; }

        [JsonProperty("text")]
        public string Text { get; set; }

        [JsonProperty("recurring")]
        public bool Recurring { get; set; }

        [JsonProperty("time")]
        public long Time { get; set; }

        [JsonProperty("complete_ts")]
        public long CompleteTs { get; set; }
    }
}
