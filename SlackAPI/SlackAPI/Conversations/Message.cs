using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace SlackAPI.Conversations
{
    public partial class Message
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("user")]
        public string User { get; set; }

        [JsonProperty("text")]
        public string Text { get; set; }

        [JsonProperty("ts")]
        public string Ts { get; set; }

        [JsonProperty("thread_ts")]
        public string ThreadTs { get; set; }

        [JsonProperty("reply_count", NullValueHandling = NullValueHandling.Ignore)]
        public long? ReplyCount { get; set; }

        [JsonProperty("replies", NullValueHandling = NullValueHandling.Ignore)]
        public List<Reply> Replies { get; set; }

        [JsonProperty("subscribed", NullValueHandling = NullValueHandling.Ignore)]
        public bool? Subscribed { get; set; }

        [JsonProperty("last_read", NullValueHandling = NullValueHandling.Ignore)]
        public string LastRead { get; set; }

        [JsonProperty("unread_count", NullValueHandling = NullValueHandling.Ignore)]
        public long? UnreadCount { get; set; }

        [JsonProperty("parent_user_id", NullValueHandling = NullValueHandling.Ignore)]
        public string ParentUserId { get; set; }

        [JsonProperty("attachments", NullValueHandling = NullValueHandling.Ignore)]
        public List<Attachment> Attachments { get; set; }

        [JsonProperty("permalink")]
        public string Permalink { get; set; }

        [JsonProperty("pinned_to")]
        public List<string> PinnedTo { get; set; }

        [JsonProperty("reactions")]
        public List<Reaction> Reactions { get; set; }

        [JsonProperty("bot_id")]
        public string BotId { get; set; }

        [JsonProperty("subtype")]
        public string Subtype { get; set; }

        [JsonProperty("username")]
        public string Username { get; set; }

        [JsonProperty("next")]
        public Message Next { get; set; }

        [JsonProperty("previous")]
        public Message Previous { get; set; }

        [JsonProperty("iid")]
        public string Iid { get; set; }
    }

    public partial class Attachment
    {
        [JsonProperty("service_name")]
        public string ServiceName { get; set; }

        [JsonProperty("text")]
        public string Text { get; set; }

        [JsonProperty("fallback")]
        public string Fallback { get; set; }

        [JsonProperty("thumb_url")]
        public string ThumbUrl { get; set; }

        [JsonProperty("thumb_width")]
        public long ThumbWidth { get; set; }

        [JsonProperty("thumb_height")]
        public long ThumbHeight { get; set; }

        [JsonProperty("id")]
        public long Id { get; set; }
    }

    public partial class Reply
    {
        [JsonProperty("user")]
        public string User { get; set; }

        [JsonProperty("ts")]
        public string Ts { get; set; }
    }

    public partial class Reaction
    {
        [JsonProperty("count")]
        public long Count { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("users")]
        public List<string> Users { get; set; }
    }
}
