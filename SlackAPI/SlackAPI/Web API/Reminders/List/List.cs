using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace SlackAPI.Reminders.List
{
    public partial class RootObject : BaseReturn
    {
        [JsonProperty("reminders")]
        public List<Reminder> Reminders { get; set; }
    }
}
