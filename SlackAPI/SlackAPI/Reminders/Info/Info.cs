using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace SlackAPI.Reminders.Info
{
    public partial class RootObject : BaseReturn
    {
        [JsonProperty("reminder")]
        public Reminder Reminder { get; set; }
    }
}
