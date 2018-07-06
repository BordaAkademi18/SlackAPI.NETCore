using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace SlackAPI.DoNotDisturb.Info
{
    public partial class DndInfo : BaseReturn
    {
        [JsonProperty("dnd_enabled")]
        public bool DndEnabled { get; set; }

        [JsonProperty("next_dnd_start_ts")]
        public long NextDndStartTs { get; set; }

        [JsonProperty("next_dnd_end_ts")]
        public long NextDndEndTs { get; set; }

        [JsonProperty("snooze_enabled")]
        public bool SnoozeEnabled { get; set; }

        [JsonProperty("snooze_endtime")]
        public long SnoozeEndtime { get; set; }

        [JsonProperty("snooze_remaining")]
        public long SnoozeRemaining { get; set; }
    }
}
