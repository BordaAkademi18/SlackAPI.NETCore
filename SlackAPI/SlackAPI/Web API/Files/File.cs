using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace SlackAPI.Files
{
    public partial class File
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("created")]
        public long Created { get; set; }

        [JsonProperty("timestamp")]
        public long Timestamp { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("mimetype")]
        public string Mimetype { get; set; }

        [JsonProperty("filetype")]
        public string Filetype { get; set; }

        [JsonProperty("pretty_type")]
        public string PrettyType { get; set; }

        [JsonProperty("user")]
        public string User { get; set; }

        [JsonProperty("mode")]
        public string Mode { get; set; }

        [JsonProperty("editable")]
        public bool Editable { get; set; }

        [JsonProperty("is_external")]
        public bool IsExternal { get; set; }

        [JsonProperty("external_type")]
        public string ExternalType { get; set; }

        [JsonProperty("username")]
        public string Username { get; set; }

        [JsonProperty("size")]
        public long Size { get; set; }

        [JsonProperty("url_private")]
        public string UrlPrivate { get; set; }

        [JsonProperty("url_private_download")]
        public string UrlPrivateDownload { get; set; }

        [JsonProperty("thumb_64")]
        public string Thumb64 { get; set; }

        [JsonProperty("thumb_80")]
        public string Thumb80 { get; set; }

        [JsonProperty("thumb_360")]
        public string Thumb360 { get; set; }

        [JsonProperty("thumb_360_gif")]
        public string Thumb360_Gif { get; set; }

        [JsonProperty("thumb_360_w")]
        public long Thumb360_W { get; set; }

        [JsonProperty("thumb_360_h")]
        public long Thumb360_H { get; set; }

        [JsonProperty("thumb_480")]
        public string Thumb480 { get; set; }

        [JsonProperty("thumb_480_w")]
        public long Thumb480_W { get; set; }

        [JsonProperty("thumb_480_h")]
        public long Thumb480_H { get; set; }

        [JsonProperty("thumb_160")]
        public string Thumb160 { get; set; }

        [JsonProperty("permalink")]
        public string Permalink { get; set; }

        [JsonProperty("permalink_public")]
        public string PermalinkPublic { get; set; }

        [JsonProperty("edit_link")]
        public string EditLink { get; set; }

        [JsonProperty("preview")]
        public string Preview { get; set; }

        [JsonProperty("preview_highlight")]
        public string PreviewHighlight { get; set; }

        [JsonProperty("lines")]
        public long Lines { get; set; }

        [JsonProperty("lines_more")]
        public long LinesMore { get; set; }

        [JsonProperty("is_public")]
        public bool IsPublic { get; set; }

        [JsonProperty("public_url_shared")]
        public bool PublicUrlShared { get; set; }

        [JsonProperty("display_as_bot")]
        public bool DisplayAsBot { get; set; }

        [JsonProperty("channels")]
        public List<string> Channels { get; set; }

        [JsonProperty("groups")]
        public List<string> Groups { get; set; }

        [JsonProperty("ims")]
        public List<string> Ims { get; set; }

        [JsonProperty("initial_comment")]
        public Comment InitialComment { get; set; }

        [JsonProperty("num_stars")]
        public long NumStars { get; set; }

        [JsonProperty("is_starred")]
        public bool IsStarred { get; set; }

        [JsonProperty("pinned_to")]
        public List<string> PinnedTo { get; set; }

        [JsonProperty("reactions")]
        public List<Reaction> Reactions { get; set; }

        [JsonProperty("comments_count")]
        public long CommentsCount { get; set; }
    }

    public partial class Comment
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("created")]
        public long Created { get; set; }

        [JsonProperty("timestamp")]
        public long Timestamp { get; set; }

        [JsonProperty("user")]
        public string User { get; set; }

        [JsonProperty("comment")]
        public string CommentText { get; set; }

        [JsonProperty("channel")]
        public string Channel { get; set; }

        [JsonProperty("reactions")]
        public List<Reaction> Reactions { get; set; }
    }

    public partial class Reaction
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("count")]
        public long Count { get; set; }

        [JsonProperty("users")]
        public List<string> Users { get; set; }
    }

    public partial class Paging
    {
        [JsonProperty("count")]
        public long Count { get; set; }

        [JsonProperty("total")]
        public long Total { get; set; }

        [JsonProperty("page")]
        public long Page { get; set; }

        [JsonProperty("pages")]
        public long Pages { get; set; }
    }
}
