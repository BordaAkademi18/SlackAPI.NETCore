using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace SlackAPI.RTM_API.Models
{
    public partial class PostMessage
    {
        [JsonProperty("text")]
        public string Text { get; set; }

        [JsonProperty("response_type", NullValueHandling = NullValueHandling.Ignore)]
        public string ResponseType { get; set; }

        [JsonProperty("attachments", NullValueHandling = NullValueHandling.Ignore)]
        public List<Attachment> Attachments { get; set; }
    }

    public partial class Attachment
    {
        [JsonProperty("title", NullValueHandling = NullValueHandling.Ignore)]
        public string Title { get; set; }

        [JsonProperty("fields", NullValueHandling = NullValueHandling.Ignore)]
        public List<Field> Fields { get; set; }

        [JsonProperty("author_name", NullValueHandling = NullValueHandling.Ignore)]
        public string AuthorName { get; set; }

        [JsonProperty("footer", NullValueHandling = NullValueHandling.Ignore)]
        public string Footer { get; set; }

        [JsonProperty("footer_icon", NullValueHandling = NullValueHandling.Ignore)]
        public string FooterIcon { get; set; }

        [JsonProperty("pretext", NullValueHandling = NullValueHandling.Ignore)]
        public string Pretext { get; set; }

        [JsonProperty("author_icon", NullValueHandling = NullValueHandling.Ignore)]
        public string AuthorIcon { get; set; }

        [JsonProperty("image_url", NullValueHandling = NullValueHandling.Ignore)]
        public string ImageUrl { get; set; }

        [JsonProperty("thumb_url", NullValueHandling = NullValueHandling.Ignore)]
        public string ThumbUrl { get; set; }

        [JsonProperty("text", NullValueHandling = NullValueHandling.Ignore)]
        public string Text { get; set; }

        [JsonProperty("fallback", NullValueHandling = NullValueHandling.Ignore)]
        public string Fallback { get; set; }

        [JsonProperty("callback_id", NullValueHandling = NullValueHandling.Ignore)]
        public string CallbackId { get; set; }

        [JsonProperty("color", NullValueHandling = NullValueHandling.Ignore)]
        public string Color { get; set; }

        [JsonProperty("attachment_type", NullValueHandling = NullValueHandling.Ignore)]
        public string AttachmentType { get; set; }

        [JsonProperty("ts", NullValueHandling = NullValueHandling.Ignore)]
        public string Ts { get; set; }

        [JsonProperty("actions", NullValueHandling = NullValueHandling.Ignore)]
        public List<Action> Actions { get; set; }
    }

    public partial class Action
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("text")]
        public string Text { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("value", NullValueHandling = NullValueHandling.Ignore)]
        public string Value { get; set; }

        [JsonProperty("data_source", NullValueHandling = NullValueHandling.Ignore)]
        public string DataSource { get; set; }

        [JsonProperty("min_query_length", NullValueHandling = NullValueHandling.Ignore)]
        public long? MinQueryLength { get; set; }

        [JsonProperty("options", NullValueHandling = NullValueHandling.Ignore)]
        public List<Option> Options { get; set; }

        [JsonProperty("selected_options", NullValueHandling = NullValueHandling.Ignore)]
        public List<Option> SelectedOptions { get; set; }
    }

    public partial class Option
    {
        [JsonProperty("text")]
        public string Text { get; set; }

        [JsonProperty("value")]
        public string Value { get; set; }
    }

    public partial class Field
    {
        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("value")]
        public string Value { get; set; }

        [JsonProperty("short")]
        public bool Short { get; set; }
    }
}
