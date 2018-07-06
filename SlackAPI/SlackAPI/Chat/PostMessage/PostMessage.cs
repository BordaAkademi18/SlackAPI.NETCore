using Newtonsoft.Json;
using SlackAPI.Conversations;
using SlackAPI.Chat;
using System;
using System.Collections.Generic;
using System.Text;

namespace SlackAPI.Chat.PostMessage
{
    public class RootObject : ChatInfo
    {
        [JsonProperty("message")]
        public Message Message { get; set; }
    }
}
