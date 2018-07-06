using RestSharp;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using SlackAPI.Users;
using SlackAPI.Conversations;
using SlackAPI.DoNotDisturb.Info;
using SlackAPI.Files;
using SlackAPI.Files.CommentResult;
using SlackAPI.Pins.List;
using SlackAPI.Reminders;
using SlackAPI.Search;
using SlackAPI.Team.Profile;
using SlackAPI.Usergroups;

namespace SlackAPI
{
    public class SlackClient
    {
        public string _OauthToken { get; private set; }

        private bool isConnected { get; set; }

        private readonly string _BaseUrl = "https://slack.com/api";

        private RestClient restClient;

        //General properties
        public Team.Team MyTeam;

        public List<User> Users;
        public List<Conversation> Channels;
        public List<Conversation> Groups;
        public List<Conversation> DirectMessages;

        public SlackClient(string OauthToken)
        {
            _OauthToken = OauthToken;
            isConnected = false;
            restClient = new RestClient(_BaseUrl);
        }

        public RestRequest QuerryBuilder(Dictionary<string, string> values, string method, Method requestType)
        {
            var request = new RestRequest(method, requestType);

            foreach (var item in values)
                request.AddParameter(item.Key, item.Value);

            switch (requestType)
            {
                case Method.GET:
                    request.AddHeader("Authorization", String.Format("Bearer {0}", _OauthToken.ToLower()));
                    break;
                case Method.POST:
                    request.AddHeader("Authorization", String.Format("Bearer {0}", _OauthToken.ToLower()));
                    break;
                case Method.PUT:
                    break;
                case Method.DELETE:
                    break;
                case Method.HEAD:
                    break;
                case Method.OPTIONS:
                    break;
                case Method.PATCH:
                    break;
                case Method.MERGE:
                    break;
                case Method.COPY:
                    break;
                default:
                    break;
            }
            return request;
        }

        public bool Connect()
        {
            var response = restClient.Execute(QuerryBuilder(new Dictionary<string, string>(), "api.test", Method.POST));
            var content = JsonConvert.DeserializeObject<BaseError>(response.Content);
            if (!content.Ok)
                return false;

            Channels = ListAllConversations(null, false, 1000, "public_channel");
            Groups = ListAllConversations(null, false, 1000, "private_channel");
            Groups = ListAllConversations(null, false, 1000, "mpim,im");
            Users = ListUsers(null, false, 1000, false);
            MyTeam = TeamInfo();
            return content.Ok;
        }

        public void ErrorHandler(string errorMessage)
        {
            switch (errorMessage)
            {
                case "account_inactive": throw new AccountInactiveException("Authentication token is for a deleted user or workspace.");
                case "already_archived": throw new AlreadyAchievedException("Channel has already been archived.");
                case "already_in_channel": throw new AlreadyInChannelException("Invited user is already in the channel.");
                case "already_pinned": throw new AlreadyPinnedException("The specified item is already pinned to the channel.");
                case "already_reacted": throw new AlreadyReactedException("The specified item already has the user/reaction combination.");
                case "as_user_not_supported": throw new AsUserNotSupportedException("The as_userparameter does not function with workspace apps.");
                case "bad_image": throw new BadImageException("The uploaded image could not be processed - try passing a JPEG, GIF or PNG");
                case "bad_timestamp": throw new BadTimestampException("Value passed for timestamp was invalid.");
                case "cannot_add_bot": throw new CannotAddBotException("Reminders can't be sent to bots.");
                case "cannot_add_others": throw new CannotAddOthersException("Guests can't set reminders for other team members.");
                case "cannot_add_others_recurring": throw new CannotAddOthersRecurringException("Recurring reminders can't be set for other team members.");
                case "cannot_add_slackbot": throw new CannotAddSlackbotException("Reminders can't be sent to Slackbot.");
                case "cannot_complete_others": throw new CannotCompleteOthersException("Reminders for other team members can't be marked complete.");
                case "cannot_complete_recurring": throw new CannotCompleteRecurringException("Recurring reminders can't be marked complete.");
                case "cannot_find_service": throw new CannotFindServiceException("A record of your app being allowed to unfurl for this workspace could not be found.");
                case "cannot_parse": throw new CannotParseException("The phrasing of the timing for this reminder is unclear. You must include a complete time description. Some examples that work: 1458678068, 20, in 5 minutes, tomorrow, at 3:30pm, on Tuesday, or next week.");
                case "cannot_prompt": throw new CannotPromptException("The current user has already interacted with and dismissed a prompt for this application.");
                case "cannot_unfurl_url": throw new CannotUnfurlUrlException("The URL cannot be unfurled. This error may be returned if you haven't acknowledged a link_shared event tied to the same URL. It is also returned when the domain appears in a workspace's administrative blacklists.");
                case "cannot_update_admin_user": throw new CannotUpdateAdminUserException("Only a primary owner can update the profile of an admin.");
                case "cant_archive_general": throw new CantAchieveGeneralException("You cannot archive the general channel");
                case "cant_delete": throw new CantDeleteException("The requested comment could not be deleted.");
                case "cant_delete_file": throw new CantDeleteFileException("Authenticated user does not have permission to delete this file.");
                case "cant_delete_message": throw new CantDeleteMessageException("Authenticated user does not have permission to delete this message.");
                case "cant_edit": throw new CantEditException("The requested file could not be found.");
                case "cant_invite": throw new CantInviteException("User cannot be invited to this channel.");
                case "cant_invite_self": throw new CantInviteSelfException("Authenticated user cannot invite themselves to a channel.");
                case "cant_kick_from_general": throw new CantKickFromGeneralException("User cannot be removed from #general.");
                case "cant_kick_self": throw new CantKickSelfException("Authenticated user can't kick themselves from a channel.");
                case "cant_leave_general": throw new CantLeaveGeneralException("Authenticated user cannot leave the general channel");
                case "cant_update_message": throw new CantUpdateMessageException("Authenticated user does not have permission to update this message.");
                case "channel_not_found": throw new ChannelNotFoundException("Value passed for channel was invalid.");
                case "compliance_exports_prevent_deletion": throw new ComplianceExportsPreventDeletionException("Compliance exports are on, messages can not be deleted");
                case "edit_window_closed": throw new EditWindowClosedException("The message cannot be edited due to the team message edit settings");
                case "fatal_error": throw new FatalErrorException("The server could not complete your operation(s) without encountering a catastrophic error. It's possible some aspect of the operation succeeded before the error was raised.");
                case "fetch_members_failed": throw new FetchMembersFailedException("Failed to fetch members for the conversation.");
                case "file_comment_not_found": throw new FileCommentNotFoundException("File comment specified by file_comment does not exist.");
                case "file_deleted": throw new FileDeletedException("The requested file was previously deleted.");
                case "file_not_found": throw new FileNotFoundException("The requested file could not be found.");
                case "file_not_shared": throw new FileNotSharedException("File specified by file is not public nor shared to the channel.");
                case "invalid_arg_name": throw new InvalidArgNameException("The method was passed an argument whose name falls outside the bounds of accepted or expected values. This includes very long names and names with non-alphanumeric characters other than _. If you get this error, it is typically an indication that you have made a very malformed API call.");
                case "invalid_array_arg": throw new InvalidArrayArgException("The method was passed a PHP-style array argument (e.g. with a name like foo[7]). These are never valid with the Slack API.");
                case "invalid_auth": throw new InvalidAuthException("Some aspect of authentication cannot be validated. Either the provided token is invalid or the request originates from an IP address disallowed from making the request.");
                case "invalid_channel": throw new InvalidChannelException("One or more channels supplied are invalid");
                case "invalid_charset": throw new InvalidCharsetException("The method was called via a POSTrequest, but the charset specified in the Content-Type header was invalid. Valid charset names are: utf-8 iso-8859-1.");
                case "invalid_cursor": throw new InvalidCursorException("Value passed for cursor was not valid or is no longer valid.");
                case "invalid_form_data": throw new InvalidFormDataException("The method was called via a POSTrequest with Content-Typeapplication/x-www-form-urlencoded or multipart/form-data, but the form data was either missing or syntactically invalid.");
                case "invalid_limit": throw new InvalidLimitException("Value passed for limit is not understood.");
                case "invalid_name": throw new InvalidNameException("Value passed for name was invalid.");
                case "invalid_name_maxlength": throw new InvalidNameMaxlengthException("Value passed for name exceeded max length.");
                case "invalid_name_punctuation": throw new InvalidNamePunctuationException("Value passed for name contained only punctuation.");
                case "invalid_name_required": throw new InvalidNameRequiredException("Value passed for name was empty.");
                case "invalid_name_specials": throw new InvalidNameSpecialsException("Value passed for name contained unallowed special characters or upper case characters.");
                case "invalid_post_type": throw new InvalidPostTypeException("The method was called via a POSTrequest, but the specified Content-Type was invalid. Valid types are: application/x-www-form-urlencodedmultipart/form-data text/plain.");
                case "invalid_presence": throw new InvalidPresenceException("Value passed for presence was invalid.");
                case "invalid_profile": throw new InvalidProfileException("Profile object passed in is not valid JSON (make sure it is URL encoded!).");
                case "invalid_ts_latest": throw new InvalidTsLatestException("Value passed for latest was invalid");
                case "invalid_ts_oldest": throw new InvalidTsOldestException("Value passed for oldest was invalid");
                case "invalid_types": throw new InvalidTypesException("Value passed for type could not be used based on the method's capabilities or the permission scopes granted to the used token.");
                case "invalid_users": throw new InvalidUsersExceptiom("Value passed for user_ids was empty or invalid.");
                case "is_archived": throw new IsArchivedException("Channel has been archived.");
                case "message_not_found": throw new MessageNotFoundException("No message exists with the requested timestamp.");
                case "method_not_supported_for_channel_type": throw new MethodNotSupportedForChannelTypeException("This type of conversation cannot be used with this method.");
                case "missing_duration": throw new MissingDurationException("No value provided for num_minutes");
                case "missing_post_type": throw new MissingPostTypeException("The method was called via a POSTrequest and included a data payload, but the request did not include a Content-Type header.");
                case "missing_scope": throw new MissingScopeException("The calling token is not granted the necessary scopes to complete this operation.");
                case "missing_unfurls": throw new MissingUnfurlsException("The request is missing the unfurls parameter.");
                case "msg_too_long": throw new MessageTooLongException("Message text is too long");
                case "name_taken": throw new NameTakenException("A channel cannot be created with the given name.");
                case "no_channel": throw new NoChannelException("Value passed for name was empty.");
                case "no_comment": throw new NoCommentException("The comment field was empty.");
                case "no_item_specified": throw new NoItemSpecifiedException("One of file, file_comment, or timestamp was not specified.");
                case "no_permission": throw new NoPermissionException("The workspace token used in this request does not have the permissions necessary to complete the request.");
                case "no_reaction": throw new NoReactionException("The specified item does not have the user/reaction combination.");
                case "no_text": throw new NoTextException("No message text provided");
                case "not_admin": throw new NotAdminException("Only admins can update the profile of another user. Some fields, like email may only be updated by an admin.");
                case "not_allowed": throw new NotAllowedException("Public sharing has been disabled for this team");
                case "not_app_admin": throw new NotApplicationAdminException("Only team owners and selected members can update the profile of a bot user.");
                case "not_authed": throw new NotAuthedException("No authentication token provided.");
                case "not_authorized": throw new NotAuthorizedException("Caller cannot rename this channel");
                case "not_enough_users": throw new NotEnoughUsersException("Needs at least 2 users to open");
                case "not_found": throw new NotFoundException("That reminder can't be found.");
                case "not_in_channel": throw new NotInChannelException("Cannot post user messages to a channel they are not in.");
                case "not_pinnable": throw new NotPinnableException("This message type is not pinnable.");
                case "not_pinned": throw new NotPinnedException("The specified item is not pinned to the channel.");
                case "org_login_required": throw new OrgLoginRequiredException("The workspace is undergoing an enterprise migration and will not be available until migration is complete.");
                case "pagination_not_available": throw new PaginationNotAvailableException("Pagination does not currently function on Enterprise Grid workspaces.");
                case "permission_denied": throw new PermissionDeniedException("The user does not have permission to add pins to the channel.");
                case "posting_to_general_channel_denied": throw new PostingToGeneralChannelDeniedException("An admin has restricted posting to the #general channel.");
                case "profile_set_failed": throw new ProfileSetFailedException("Failed to set user profile.");
                case "rate_limited": throw new RateLimitedException("Application has posted too many messages, read the Rate Limit documentation for more information");
                case "request_timeout": throw new RequestTimeoutException("The method was called via a POSTrequest, but the POST data was either missing or truncated.");
                case "reserved_name": throw new ReservedNameException("First or last name are reserved.");
                case "restricted_action": throw new RestrictedActionException("A workspace preference prevents the authenticated user from posting.");
                case "restricted_action_non_threadable_channel": throw new RestrictedActionNonThreadableChannelException("Cannot post thread replies into a non_threadable channel.");
                case "restricted_action_read_only_channel": throw new RestrictedActionReadonlyChannelException("Cannot post any message into a read-only channel.");
                case "restricted_action_thread_only_channel": throw new RestrictedActionThreadonlyChannelException("Cannot post top-level messages into a thread-only channel.");
                case "snooze_end_failed": throw new SnoozeEndFailedException("There was a problem setting the user's Do Not Disturb status");
                case "snooze_failed": throw new SnoozeFailedException("There was a problem setting the user's Do Not Disturb status");
                case "snooze_not_active": throw new SnoozeNotActiveException("Snooze is not active for this user and cannot be ended");
                case "team_added_to_org": throw new TeamAddedToOrgException("The workspace associated with your request is currently undergoing migration to an Enterprise Organization. Web API and other platform operations will be intermittently unavailable until the transition is complete.");
                case "thread_not_found": throw new ThreadNotFoundException("Value for ts was missing or invalid.");
                case "token_revoked": throw new TokenRevokedException("Authentication token is for a deleted user or workspace or the app has been removed.");
                case "too_large": throw new TooLargeImageException("The uploaded image had excessive dimensions");
                case "too_long": throw new TooLongTextException("Purpose was longer than 250 characters.");
                case "too_many_attachments": throw new TooManyAttachmentsException("Too many attachments were provided with this message. A maximum of 100 attachments are allowed on a message.");
                case "too_many_convos_for_app_on_team": throw new TooManyConvosForAppOnTeamException("This app has exceeded its per-workspace limit of public and private channels.");
                case "too_many_convos_for_team": throw new TooManyConvosForTeamException("The workspace has exceeded its limit of public and private channels.");
                case "too_many_emoji": throw new TooManyEmoji("The limit for distinct reactions (i.e emoji) on the item has been reached.");
                case "too_many_frames": throw new TooManyFrames("An animated GIF with too many frames was uploaded");
                case "too_many_reactions": throw new TooManyReactions("The limit for reactions a person may add to the item has been reached.");
                case "too_many_users": throw new TooManyUsers("Needs at most 8 users to open");
                case "unknown_error": throw new UnknownErrorException("There was a mysterious problem ending the user's Do Not Disturb session");
                case "unknown_type": throw new UnknownTypeException("Value passed for types was invalid");
                case "ura_max_channels": throw new URAMaxChannelsException("URA is already in the maximum number of channels.");
                case "user_disabled": throw new UserDisabledException("A specified user has been disabled.");
                case "user_does_not_own_channel": throw new UserDoesNotOwnChannelException("Calling user does not own this DM channel.");
                case "user_is_bot": throw new UserIsBotException("This method cannot be called by a bot user.");
                case "user_is_restricted": throw new UserIsRestrictedException("This method cannot be called by a restricted user or single channel guest.");
                case "user_is_ultra_restricted": throw new UserIsUltraRestrictedException("This method cannot be called by a single channel guest.");
                case "user_not_found": throw new UserNotFoundException("One or more users in user_ids was not found.");
                case "user_not_in_channel": throw new UserNotInChannelException("Intended recipient is not in the specified channel.");
                case "user_not_visible": throw new UserNotFoundException("The calling user is restricted from seeing the requested user.");
                case "users_list_not_supplied": throw new UsersListNotSuppliedException("Missing users in request");
                case "users_not_found": throw new UsersNotFoundException("Value passed for user was invalid.");
                default:
                    break;
            }
        }

        #region Conversation Group

        public List<Conversation> ListAllConversations(string cursor = null, bool exclude_achieved = false, int limit = 100, string types = "public_channel")
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            dict.Add("cursor", cursor);
            dict.Add("exclude_achieved", exclude_achieved.ToString());
            dict.Add("limit", limit.ToString());
            dict.Add("types", types);
            var request = QuerryBuilder(dict, "conversations.list", Method.GET);
            var response = restClient.Execute(request);
            var content = JsonConvert.DeserializeObject<Conversations.List.RootObject>(response.Content);
            BaseError contentError;
            if (content.Ok == false)
            {
                contentError = JsonConvert.DeserializeObject<BaseError>(response.Content);
                ErrorHandler(contentError.Error);
            }
            return content.Channels;
        }

        public bool AchieveConversation(string channel)
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            dict.Add("channel", channel);
            var request = QuerryBuilder(dict, "conversations.achieve", Method.POST);
            var response = restClient.Execute(request);
            var content = JsonConvert.DeserializeObject<BaseError>(response.Content);
            BaseError contentError;
            if (content.Ok == false)
            {
                contentError = JsonConvert.DeserializeObject<BaseError>(response.Content);
                ErrorHandler(contentError.Error);
            }
            return content.Ok;
        }

        public bool CloseConversation(string channel)
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            dict.Add("channel", channel);
            var request = QuerryBuilder(dict, "conversations.close", Method.POST);
            var response = restClient.Execute(request);
            var content = JsonConvert.DeserializeObject<Conversations.Close.RootObject>(response.Content);
            BaseError contentError;
            if (content.Ok == false)
            {
                contentError = JsonConvert.DeserializeObject<BaseError>(response.Content);
                ErrorHandler(contentError.Error);
            }
            return content.Ok;
        }

        public Conversation CreateConversation(string name, bool is_private = false, string user_ids = null)
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            dict.Add("name", name);
            dict.Add("is_private", is_private.ToString());
            dict.Add("user_ids", user_ids);
            var request = QuerryBuilder(dict, "conversations.create", Method.POST);
            var response = restClient.Execute(request);
            var content = JsonConvert.DeserializeObject<Conversations.Info.RootObject>(response.Content);
            BaseError contentError;
            if (content.Ok == false)
            {
                contentError = JsonConvert.DeserializeObject<BaseError>(response.Content);
                ErrorHandler(contentError.Error);
            }
            return content.Channel;
        }

        public List<Message> HistoryConversation(string channel, string cursor = null, bool inclusive = false, string latest = null, int limit = 20, string oldest = "0")
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            dict.Add("channel", channel);
            dict.Add("cursor", cursor);
            dict.Add("inclusive", inclusive.ToString());
            dict.Add("latest", latest);
            dict.Add("limit", limit.ToString());
            dict.Add("oldest", oldest);
            var request = QuerryBuilder(dict, "conversations.history", Method.GET);
            var response = restClient.Execute(request);
            var content = JsonConvert.DeserializeObject<Conversations.History.RootObject>(response.Content);
            BaseError contentError;
            if (content.Ok == false)
            {
                contentError = JsonConvert.DeserializeObject<BaseError>(response.Content);
                ErrorHandler(contentError.Error);
            }
            return content.Messages;
        }

        public Conversation InfoConversation(string channel, string cursor = null, bool include_locale = false)
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            dict.Add("channel", channel);
            dict.Add("include_locale", include_locale.ToString());
            var request = QuerryBuilder(dict, "conversations.info", Method.GET);
            var response = restClient.Execute(request);
            var content = JsonConvert.DeserializeObject<Conversations.Info.RootObject>(response.Content);
            BaseError contentError;
            if (content.Ok == false)
            {
                contentError = JsonConvert.DeserializeObject<BaseError>(response.Content);
                ErrorHandler(contentError.Error);
            }
            return content.Channel;
        }

        public Conversation InviteToConversation(string channel, List<string> users = null)
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            dict.Add("channel", channel);
            dict.Add("users", Extension.BuildString(users));
            var request = QuerryBuilder(dict, "conversations.invite", Method.POST);
            var response = restClient.Execute(request);
            var content = JsonConvert.DeserializeObject<Conversations.Info.RootObject>(response.Content);
            BaseError contentError;
            if (content.Ok == false)
            {
                contentError = JsonConvert.DeserializeObject<BaseError>(response.Content);
                ErrorHandler(contentError.Error);
            }
            return content.Channel;
        }

        public Conversation JoinConversation(string channel)
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            dict.Add("channel", channel);
            var request = QuerryBuilder(dict, "conversations.join", Method.POST);
            var response = restClient.Execute(request);
            var content = JsonConvert.DeserializeObject<Conversations.Join.RootObject>(response.Content);
            BaseError contentError;
            if (content.Ok == false)
            {
                contentError = JsonConvert.DeserializeObject<BaseError>(response.Content);
                ErrorHandler(contentError.Error);
            }
            return content.Channel;
        }

        public bool KickConversation(string channel, string user)
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            dict.Add("channel", channel);
            dict.Add("user", user);
            var request = QuerryBuilder(dict, "conversations.kick", Method.POST);
            var response = restClient.Execute(request);
            var content = JsonConvert.DeserializeObject<BaseError>(response.Content);
            BaseError contentError;
            if (content.Ok == false)
            {
                contentError = JsonConvert.DeserializeObject<BaseError>(response.Content);
                ErrorHandler(contentError.Error);
            }
            return content.Ok;
        }

        public bool LeaveConversation(string channel)
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            dict.Add("channel", channel);
            var request = QuerryBuilder(dict, "conversations.leave", Method.POST);
            var response = restClient.Execute(request);
            var content = JsonConvert.DeserializeObject<BaseError>(response.Content);
            BaseError contentError;
            if (content.Ok == false)
            {
                contentError = JsonConvert.DeserializeObject<BaseError>(response.Content);
                ErrorHandler(contentError.Error);
            }
            return content.Ok;
        }

        public List<string> MembersOfConversation(string channel, string cursor = null, int limit = 20)
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            dict.Add("channel", channel);
            dict.Add("cursor", cursor);
            dict.Add("limit", limit.ToString());
            var request = QuerryBuilder(dict, "conversations.members", Method.GET);
            var response = restClient.Execute(request);
            var content = JsonConvert.DeserializeObject<Conversations.Members.RootObject>(response.Content);
            BaseError contentError;
            if (content.Ok == false)
            {
                contentError = JsonConvert.DeserializeObject<BaseError>(response.Content);
                ErrorHandler(contentError.Error);
            }
            return content.Members;
        }

        public Conversation OpenConversation(string channel = null, bool return_im = false, List<string> users = null)
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            dict.Add("channel", channel);
            dict.Add("return_im", return_im.ToString());
            dict.Add("users", Extension.BuildString(users));
            var request = QuerryBuilder(dict, "conversations.open", Method.POST);
            var response = restClient.Execute(request);
            var content = JsonConvert.DeserializeObject<Conversations.Open.RootObject>(response.Content);
            BaseError contentError;
            if (content.Ok == false)
            {
                contentError = JsonConvert.DeserializeObject<BaseError>(response.Content);
                ErrorHandler(contentError.Error);
            }
            return content.Channel;
        }

        public Conversation RenameConversation(string channel, string name)
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            dict.Add("channel", channel);
            dict.Add("name", name);
            var request = QuerryBuilder(dict, "conversations.rename", Method.POST);
            var response = restClient.Execute(request);
            var content = JsonConvert.DeserializeObject<Conversations.Info.RootObject>(response.Content);
            BaseError contentError;
            if (content.Ok == false)
            {
                contentError = JsonConvert.DeserializeObject<BaseError>(response.Content);
                ErrorHandler(contentError.Error);
            }
            return content.Channel;
        }

        public List<Message> RepliesToMessage(string channel, string ts, string cursor = null, bool inclusive = false, string latest = null, int limit = 20, string oldest = "0")
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            dict.Add("channel", channel);
            dict.Add("ts", ts);
            dict.Add("cursor", cursor);
            dict.Add("inclusive", inclusive.ToString());
            dict.Add("latest", latest);
            dict.Add("limit", limit.ToString());
            dict.Add("oldest", oldest);
            var request = QuerryBuilder(dict, "conversations.replies", Method.GET);
            var response = restClient.Execute(request);
            var content = JsonConvert.DeserializeObject<Conversations.Replies.RootObject>(response.Content);
            BaseError contentError;
            if (content.Ok == false)
            {
                contentError = JsonConvert.DeserializeObject<BaseError>(response.Content);
                ErrorHandler(contentError.Error);
            }
            return content.Messages;
        }

        public string SetPurposeOfConversation(string channel, string purpose)
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            dict.Add("channel", channel);
            dict.Add("purpose", purpose);
            var request = QuerryBuilder(dict, "conversations.setPurpose", Method.POST);
            var response = restClient.Execute(request);
            var content = JsonConvert.DeserializeObject<Conversations.SetPurpose.RootObject>(response.Content);
            BaseError contentError;
            if (content.Ok == false)
            {
                contentError = JsonConvert.DeserializeObject<BaseError>(response.Content);
                ErrorHandler(contentError.Error);
            }
            return content.Purpose;
        }

        public string SetTopicOfConversation(string channel, string topic)
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            dict.Add("channel", channel);
            dict.Add("topic", topic);
            var request = QuerryBuilder(dict, "conversations.setTopic", Method.POST);
            var response = restClient.Execute(request);
            var content = JsonConvert.DeserializeObject<Conversations.SetPurpose.RootObject>(response.Content);
            BaseError contentError;
            if (content.Ok == false)
            {
                contentError = JsonConvert.DeserializeObject<BaseError>(response.Content);
                ErrorHandler(contentError.Error);
            }
            return content.Purpose;
        }

        public bool UnachieveConversation(string channel)
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            dict.Add("channel", channel);
            var request = QuerryBuilder(dict, "conversations.unachieve", Method.POST);
            var response = restClient.Execute(request);
            var content = JsonConvert.DeserializeObject<BaseError>(response.Content);
            BaseError contentError;
            if (content.Ok == false)
            {
                contentError = JsonConvert.DeserializeObject<BaseError>(response.Content);
                ErrorHandler(contentError.Error);
            }
            return content.Ok;
        }
        #endregion

        #region Chat Group
        public bool DeleteMessage(string channel, DateTime ts, bool as_user = false)
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            dict.Add("channel", channel);
            dict.Add("ts", ts.ToProperTimeStamp());
            dict.Add("as_user", as_user.ToString());
            var request = QuerryBuilder(dict, "chat.delete", Method.GET);
            var response = restClient.Execute(request);
            var content = JsonConvert.DeserializeObject<Chat.ChatInfo>(response.Content);
            BaseError contentError;
            if (content.Ok == false)
            {
                contentError = JsonConvert.DeserializeObject<BaseError>(response.Content);
                ErrorHandler(contentError.Error);
            }
            return content.Ok;
        }

        public string GetPermalink(string channel, string message_ts)
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            dict.Add("channel", channel);
            dict.Add("message_ts", message_ts);
            var request = QuerryBuilder(dict, "chat.getPermalink", Method.GET);
            var response = restClient.Execute(request);
            var content = JsonConvert.DeserializeObject<Chat.GetPermalink.RootObject>(response.Content);
            BaseError contentError;
            if (content.Ok == false)
            {
                contentError = JsonConvert.DeserializeObject<BaseError>(response.Content);
                ErrorHandler(contentError.Error);
            }
            return content.Permalink;
        }

        public bool MeMessage(string channel, string text)
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            dict.Add("channel", channel);
            dict.Add("text", text);
            var request = QuerryBuilder(dict, "chat.meMessage", Method.POST);
            var response = restClient.Execute(request);
            var content = JsonConvert.DeserializeObject<Chat.ChatInfo>(response.Content);
            BaseError contentError;
            if (content.Ok == false)
            {
                contentError = JsonConvert.DeserializeObject<BaseError>(response.Content);
                ErrorHandler(contentError.Error);
            }
            return content.Ok;
        }

        public bool PostEphemeral(string channel, string text, string user, bool as_user = false, string attachments = null, bool link_names = true, string parse = "full")
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            dict.Add("channel", channel);
            dict.Add("text", text);
            dict.Add("user", user);
            dict.Add("as_user", as_user.ToString());
            dict.Add("attachments", attachments);
            dict.Add("link_names", link_names.ToString());
            dict.Add("parse", parse);
            var request = QuerryBuilder(dict, "chat.postEphemeral", Method.POST);
            var response = restClient.Execute(request);
            var content = JsonConvert.DeserializeObject<Chat.PostEphemeral.RootObject>(response.Content);
            BaseError contentError;
            if (content.Ok == false)
            {
                contentError = JsonConvert.DeserializeObject<BaseError>(response.Content);
                ErrorHandler(contentError.Error);
            }
            return content.Ok;
        }

        public Message PostMessage(string channel, string text, bool as_user = false,
            string attachments = null, string icon_emoji = null, string icon_url = null, bool link_names = true,
            bool mrkdwn = true, string parse = "full", bool reply_broadcast = false, string thread_ts = null,
            bool unfurl_links = true, bool unfurl_media = true, string username = null)
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            dict.Add("channel", channel);
            dict.Add("text", text);
            dict.Add("as_user", as_user.ToString());
            dict.Add("attachments", attachments);
            dict.Add("icon_emoji", icon_emoji);
            dict.Add("icon_url", icon_url);
            dict.Add("link_names", link_names.ToString());
            dict.Add("mrkdwn", mrkdwn.ToString());
            dict.Add("parse", parse);
            dict.Add("reply_broadcast", reply_broadcast.ToString());
            dict.Add("thread_ts", thread_ts);
            dict.Add("unfurl_links", unfurl_links.ToString());
            dict.Add("unfurl_media", unfurl_media.ToString());
            dict.Add("username", username);
            var request = QuerryBuilder(dict, "chat.postMessage", Method.POST);
            var response = restClient.Execute(request);
            var content = JsonConvert.DeserializeObject<Chat.PostMessage.RootObject>(response.Content);
            BaseError contentError;
            if (content.Ok == false)
            {
                contentError = JsonConvert.DeserializeObject<BaseError>(response.Content);
                ErrorHandler(contentError.Error);
            }
            return content.Message;
        }

        public bool UpdateMessage(string channel, string text, DateTime ts, bool as_user = false, string attachments = null, string link_names = "none", string parse = "none")
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            dict.Add("channel", channel);
            dict.Add("text", text);
            dict.Add("ts", ts.ToProperTimeStamp());
            dict.Add("as_user", as_user.ToString());
            dict.Add("attachments", attachments);
            dict.Add("link_names", link_names);
            dict.Add("parse", parse);
            var request = QuerryBuilder(dict, "chat.update", Method.POST);
            var response = restClient.Execute(request);
            var content = JsonConvert.DeserializeObject<Chat.ChatInfo>(response.Content);
            BaseError contentError;
            if (content.Ok == false)
            {
                contentError = JsonConvert.DeserializeObject<BaseError>(response.Content);
                ErrorHandler(contentError.Error);
            }
            return content.Ok;
        }
        #endregion

        #region DoNotDisturb Group
        public bool EndDnd()
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            var request = QuerryBuilder(dict, "dnd.endDnd", Method.POST);
            var response = restClient.Execute(request);
            var content = JsonConvert.DeserializeObject<BaseError>(response.Content);
            BaseError contentError;
            if (content.Ok == false)
            {
                contentError = JsonConvert.DeserializeObject<BaseError>(response.Content);
                ErrorHandler(contentError.Error);
            }
            return content.Ok;
        }

        public bool EndSnooze()
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            var request = QuerryBuilder(dict, "dnd.endSnooze", Method.POST);
            var response = restClient.Execute(request);
            var content = JsonConvert.DeserializeObject<DoNotDisturb.Info.DndInfo>(response.Content);
            BaseError contentError;
            if (content.Ok == false)
            {
                contentError = JsonConvert.DeserializeObject<BaseError>(response.Content);
                ErrorHandler(contentError.Error);
            }
            return !content.SnoozeEnabled;
        }

        public DoNotDisturb.Info.DndInfo DndInfo(string user = null)
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            dict.Add("user", user);
            var request = QuerryBuilder(dict, "dnd.info", Method.GET);
            var response = restClient.Execute(request);
            var content = JsonConvert.DeserializeObject<DoNotDisturb.Info.DndInfo>(response.Content);
            BaseError contentError;
            if (content.Ok == false)
            {
                contentError = JsonConvert.DeserializeObject<BaseError>(response.Content);
                ErrorHandler(contentError.Error);
            }
            return content;
        }

        public DoNotDisturb.Info.DndInfo SetSnooze(int num_minutes)
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            dict.Add("num_minutes", num_minutes.ToString());
            var request = QuerryBuilder(dict, "dnd.setSnooze", Method.GET);
            var response = restClient.Execute(request);
            var content = JsonConvert.DeserializeObject<DoNotDisturb.Info.DndInfo>(response.Content);
            BaseError contentError;
            if (content.Ok == false)
            {
                contentError = JsonConvert.DeserializeObject<BaseError>(response.Content);
                ErrorHandler(contentError.Error);
            }
            return content;
        }

        public Dictionary<string, DndInfo> DndTeamInfo(List<string> users = null)
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            dict.Add("users", Extension.BuildString(users));
            var request = QuerryBuilder(dict, "dnd.teamInfo", Method.GET);
            var response = restClient.Execute(request);
            var content = JsonConvert.DeserializeObject<DoNotDisturb.TeamInfo.RootObject>(response.Content);
            BaseError contentError;
            if (content.Ok == false)
            {
                contentError = JsonConvert.DeserializeObject<BaseError>(response.Content);
                ErrorHandler(contentError.Error);
            }
            return content.Users;
        }
        #endregion

        #region Files Group
        public bool DeleteFileComment(string file, string id)
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            dict.Add("file", file);
            dict.Add("id", id);
            var request = QuerryBuilder(dict, "files.comment.delete", Method.POST);
            var response = restClient.Execute(request);
            var content = JsonConvert.DeserializeObject<BaseError>(response.Content);
            BaseError contentError;
            if (content.Ok == false)
            {
                contentError = JsonConvert.DeserializeObject<BaseError>(response.Content);
                ErrorHandler(contentError.Error);
            }
            return content.Ok;
        }

        public Comment EditFileComment(string comment, string file, string id)
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            dict.Add("comment", comment);
            dict.Add("file", file);
            dict.Add("id", id);
            var request = QuerryBuilder(dict, "files.comment.edit", Method.POST);
            var response = restClient.Execute(request);
            var content = JsonConvert.DeserializeObject<CommentResult>(response.Content);
            BaseError contentError;
            if (content.Ok == false)
            {
                contentError = JsonConvert.DeserializeObject<BaseError>(response.Content);
                ErrorHandler(contentError.Error);
            }
            return content.Comment;
        }

        public Comment AddFileComment(string comment, string file)
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            dict.Add("comment", comment);
            dict.Add("file", file);
            var request = QuerryBuilder(dict, "files.comment.add", Method.POST);
            var response = restClient.Execute(request);
            var content = JsonConvert.DeserializeObject<CommentResult>(response.Content);
            BaseError contentError;
            if (content.Ok == false)
            {
                contentError = JsonConvert.DeserializeObject<BaseError>(response.Content);
                ErrorHandler(contentError.Error);
            }
            return content.Comment;
        }

        public File FileInfo(string file, int count = 100, int page = 1)
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            dict.Add("file", file);
            dict.Add("count", count.ToString());
            dict.Add("page", page.ToString());
            var request = QuerryBuilder(dict, "files.info", Method.GET);
            var response = restClient.Execute(request);
            var content = JsonConvert.DeserializeObject<FileResult>(response.Content);
            BaseError contentError;
            if (content.Ok == false)
            {
                contentError = JsonConvert.DeserializeObject<BaseError>(response.Content);
                ErrorHandler(contentError.Error);
            }
            return content.File;
        }

        public bool DeleteFile(string file)
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            dict.Add("file", file);
            var request = QuerryBuilder(dict, "files.delete", Method.POST);
            var response = restClient.Execute(request);
            var content = JsonConvert.DeserializeObject<BaseError>(response.Content);
            BaseError contentError;
            if (content.Ok == false)
            {
                contentError = JsonConvert.DeserializeObject<BaseError>(response.Content);
                ErrorHandler(contentError.Error);
            }
            return content.Ok;
        }

        public List<File> ListFiles(string channel = null, int count = 100, int page = 1, string ts_from = "0",
            string ts_to = "now", string types = "all", string user = null)
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            dict.Add("channel", channel);
            dict.Add("count", count.ToString());
            dict.Add("page", page.ToString());
            dict.Add("ts_from", ts_from);
            dict.Add("ts_to", ts_to);
            dict.Add("types", types);
            dict.Add("user", user);
            var request = QuerryBuilder(dict, "files.list", Method.GET);
            var response = restClient.Execute(request);
            var content = JsonConvert.DeserializeObject<Files.List.RootObject>(response.Content);
            BaseError contentError;
            if (content.Ok == false)
            {
                contentError = JsonConvert.DeserializeObject<BaseError>(response.Content);
                ErrorHandler(contentError.Error);
            }
            return content.Files;
        }

        public File RevokePublicUrl(string file)
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            dict.Add("file", file);
            var request = QuerryBuilder(dict, "files.revokePublicURL", Method.POST);
            var response = restClient.Execute(request);
            var content = JsonConvert.DeserializeObject<FileResult>(response.Content);
            BaseError contentError;
            if (content.Ok == false)
            {
                contentError = JsonConvert.DeserializeObject<BaseError>(response.Content);
                ErrorHandler(contentError.Error);
            }
            return content.File;
        }

        public File SharedPublicUrl(string file)
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            dict.Add("file", file);
            var request = QuerryBuilder(dict, "files.sharedPublicURL", Method.POST);
            var response = restClient.Execute(request);
            var content = JsonConvert.DeserializeObject<Files.SharedPublicUrl.RootObject>(response.Content);
            BaseError contentError;
            if (content.Ok == false)
            {
                contentError = JsonConvert.DeserializeObject<BaseError>(response.Content);
                ErrorHandler(contentError.Error);
            }
            return content.File;
        }

        public File UploadFile(string channels = null, string content = null, string file = null, string filename = null, string filetype = null,
            string initial_comment = null, string title = null)
        {
            if ((content == null && file == null) || (content != null && file != null))
                return null;
            Dictionary<string, string> dict = new Dictionary<string, string>();
            dict.Add("channels", channels);
            dict.Add("content", content);
            dict.Add("file", file);
            dict.Add("filename", filename);
            dict.Add("filetype", filetype);
            dict.Add("initial_comment", initial_comment);
            dict.Add("title", title);
            var request = QuerryBuilder(dict, "files.upload", Method.POST);
            var response = restClient.Execute(request);
            var contentRes = JsonConvert.DeserializeObject<FileResult>(response.Content);
            BaseError contentError;
            if (contentRes.Ok == false)
            {
                contentError = JsonConvert.DeserializeObject<BaseError>(response.Content);
                ErrorHandler(contentError.Error);
            }
            return contentRes.File;
        }
        #endregion

        #region Pins Group
        public bool AddPins(string channel, string file = null, string file_comment = null, string timestamp = null)
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            dict.Add("channel", channel);
            dict.Add("file", file);
            dict.Add("file_comment", file_comment);
            dict.Add("timestamp", timestamp);
            var request = QuerryBuilder(dict, "pins.add", Method.POST);
            var response = restClient.Execute(request);
            var content = JsonConvert.DeserializeObject<BaseError>(response.Content);
            BaseError contentError;
            if (content.Ok == false)
            {
                contentError = JsonConvert.DeserializeObject<BaseError>(response.Content);
                ErrorHandler(contentError.Error);
            }
            return content.Ok;
        }

        public List<Item> ListPins(string channel)
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            dict.Add("channel", channel);
            var request = QuerryBuilder(dict, "pins.list", Method.GET);
            var response = restClient.Execute(request);
            var content = JsonConvert.DeserializeObject<Pins.List.RootObject>(response.Content);
            BaseError contentError;
            if (content.Ok == false)
            {
                contentError = JsonConvert.DeserializeObject<BaseError>(response.Content);
                ErrorHandler(contentError.Error);
            }
            return content.Items;
        }

        public bool RemovePins(string channel, string file = null, string file_comment = null, string timestamp = null)
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            dict.Add("channel", channel);
            dict.Add("file", file);
            dict.Add("file_comment", file_comment);
            dict.Add("timestamp", timestamp);
            var request = QuerryBuilder(dict, "pins.remove", Method.POST);
            var response = restClient.Execute(request);
            var content = JsonConvert.DeserializeObject<BaseError>(response.Content);
            BaseError contentError;
            if (content.Ok == false)
            {
                contentError = JsonConvert.DeserializeObject<BaseError>(response.Content);
                ErrorHandler(contentError.Error);
            }
            return content.Ok;
        }
        #endregion

        #region Reactions Group
        public bool AddReactions(string name, string channel = null, string file = null, string file_comment = null, string timestamp = null)
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            dict.Add("name", name);
            dict.Add("channel", channel);
            dict.Add("file", file);
            dict.Add("file_comment", file_comment);
            dict.Add("timestamp", timestamp);
            var request = QuerryBuilder(dict, "reactions.add", Method.POST);
            var response = restClient.Execute(request);
            var content = JsonConvert.DeserializeObject<BaseError>(response.Content);
            BaseError contentError;
            if (content.Ok == false)
            {
                contentError = JsonConvert.DeserializeObject<BaseError>(response.Content);
                ErrorHandler(contentError.Error);
            }
            return content.Ok;
        }

        public File GetReactions(string channel = null, string file = null, string file_comment = null, string timestamp = null)
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            dict.Add("channel", channel);
            dict.Add("file", file);
            dict.Add("file_comment", file_comment);
            dict.Add("timestamp", timestamp);
            var request = QuerryBuilder(dict, "reactions.add", Method.GET);
            var response = restClient.Execute(request);
            var content = JsonConvert.DeserializeObject<Reactions.Get.RootObject>(response.Content);
            BaseError contentError;
            if (content.Ok == false)
            {
                contentError = JsonConvert.DeserializeObject<BaseError>(response.Content);
                ErrorHandler(contentError.Error);
            }
            return content.File;
        }

        public List<Reactions.List.Item> ListReactions(int count = 100, bool full = true, int page = 1, string user = null)
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            dict.Add("count", count.ToString());
            dict.Add("full", full.ToString());
            dict.Add("page", page.ToString());
            dict.Add("user", user);
            var request = QuerryBuilder(dict, "reactions.list", Method.GET);
            var response = restClient.Execute(request);
            var content = JsonConvert.DeserializeObject<Reactions.List.RootObject>(response.Content);
            BaseError contentError;
            if (content.Ok == false)
            {
                contentError = JsonConvert.DeserializeObject<BaseError>(response.Content);
                ErrorHandler(contentError.Error);
            }
            return content.Items;
        }

        public bool RemoveReactions(string name, string channel = null, string file = null, string file_comment = null, string timestamp = null)
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            dict.Add("name", name);
            dict.Add("channel", channel);
            dict.Add("file", file);
            dict.Add("file_comment", file_comment);
            dict.Add("timestamp", timestamp);
            var request = QuerryBuilder(dict, "reactions.add", Method.POST);
            var response = restClient.Execute(request);
            var content = JsonConvert.DeserializeObject<BaseError>(response.Content);
            BaseError contentError;
            if (content.Ok == false)
            {
                contentError = JsonConvert.DeserializeObject<BaseError>(response.Content);
                ErrorHandler(contentError.Error);
            }
            return content.Ok;
        }
        #endregion

        #region Reminders Group
        public Reminder AddReminders(string text, DateTime time, string user = null)
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            dict.Add("text", text);
            dict.Add("time", time.ToProperTimeStamp());
            dict.Add("user", user);
            var request = QuerryBuilder(dict, "reminders.add", Method.POST);
            var response = restClient.Execute(request);
            var content = JsonConvert.DeserializeObject<Reminders.Info.RootObject>(response.Content);
            BaseError contentError;
            if (content.Ok == false)
            {
                contentError = JsonConvert.DeserializeObject<BaseError>(response.Content);
                ErrorHandler(contentError.Error);
            }
            return content.Reminder;
        }

        public bool CompleteReminder(string text, string reminder)
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            dict.Add("text", text);
            dict.Add("reminder", reminder);
            var request = QuerryBuilder(dict, "reminders.complete", Method.POST);
            var response = restClient.Execute(request);
            var content = JsonConvert.DeserializeObject<BaseError>(response.Content);
            BaseError contentError;
            if (content.Ok == false)
            {
                contentError = JsonConvert.DeserializeObject<BaseError>(response.Content);
                ErrorHandler(contentError.Error);
            }
            return content.Ok;
        }

        public bool DeleteReminder(string text, string reminder)
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            dict.Add("text", text);
            dict.Add("reminder", reminder);
            var request = QuerryBuilder(dict, "reminders.delete", Method.POST);
            var response = restClient.Execute(request);
            var content = JsonConvert.DeserializeObject<BaseError>(response.Content);
            BaseError contentError;
            if (content.Ok == false)
            {
                contentError = JsonConvert.DeserializeObject<BaseError>(response.Content);
                ErrorHandler(contentError.Error);
            }
            return content.Ok;
        }

        public Reminder ReminderInfo(string text, string reminder)
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            dict.Add("text", text);
            dict.Add("reminder", reminder);
            var request = QuerryBuilder(dict, "reminders.info", Method.GET);
            var response = restClient.Execute(request);
            var content = JsonConvert.DeserializeObject<Reminders.Info.RootObject>(response.Content);
            BaseError contentError;
            if (content.Ok == false)
            {
                contentError = JsonConvert.DeserializeObject<BaseError>(response.Content);
                ErrorHandler(contentError.Error);
            }
            return content.Reminder;
        }

        public List<Reminder> ListReminders()
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            var request = QuerryBuilder(dict, "reminders.list", Method.GET);
            var response = restClient.Execute(request);
            var content = JsonConvert.DeserializeObject<Reminders.List.RootObject>(response.Content);
            BaseError contentError;
            if (content.Ok == false)
            {
                contentError = JsonConvert.DeserializeObject<BaseError>(response.Content);
                ErrorHandler(contentError.Error);
            }
            return content.Reminders;
        }
        #endregion

        #region Search Group
        public Match<File> SearchAllFiles(string query, int count = 20, bool highlight = false, int page = 1,
            string sort = "score", string sort_dir = "desc")
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            dict.Add("query", query);
            dict.Add("count", count.ToString());
            dict.Add("highlight", highlight.ToString());
            dict.Add("page", page.ToString());
            dict.Add("sort", sort);
            dict.Add("sort_dir", sort_dir);
            var request = QuerryBuilder(dict, "search.all", Method.GET);
            var response = restClient.Execute(request);
            var content = JsonConvert.DeserializeObject<Search.RootObject>(response.Content);
            BaseError contentError;
            if (content.Ok == false)
            {
                contentError = JsonConvert.DeserializeObject<BaseError>(response.Content);
                ErrorHandler(contentError.Error);
            }
            return content.Files;
        }

        public Match<Message> SearchAllMessages(string query, int count = 20, bool highlight = false, int page = 1,
            string sort = "score", string sort_dir = "desc")
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            dict.Add("query", query);
            dict.Add("count", count.ToString());
            dict.Add("highlight", highlight.ToString());
            dict.Add("page", page.ToString());
            dict.Add("sort", sort);
            dict.Add("sort_dir", sort_dir);
            var request = QuerryBuilder(dict, "search.all", Method.GET);
            var response = restClient.Execute(request);
            var content = JsonConvert.DeserializeObject<Search.RootObject>(response.Content);
            BaseError contentError;
            if (content.Ok == false)
            {
                contentError = JsonConvert.DeserializeObject<BaseError>(response.Content);
                ErrorHandler(contentError.Error);
            }
            return content.Messages;
        }
        #endregion

        #region Team Group
        public Team.Team TeamInfo()
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            var request = QuerryBuilder(dict, "team.info", Method.GET);
            var response = restClient.Execute(request);
            var content = JsonConvert.DeserializeObject<Team.Info.RootObject>(response.Content);
            BaseError contentError;
            if (content.Ok == false)
            {
                contentError = JsonConvert.DeserializeObject<BaseError>(response.Content);
                ErrorHandler(contentError.Error);
            }
            return content.Team;
        }

        public List<Field> GetTeamProfile(string visibility = "all")
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            var request = QuerryBuilder(dict, "team.profile.get", Method.GET);
            var response = restClient.Execute(request);
            var content = JsonConvert.DeserializeObject<Team.Profile.RootObject>(response.Content);
            BaseError contentError;
            if (content.Ok == false)
            {
                contentError = JsonConvert.DeserializeObject<BaseError>(response.Content);
                ErrorHandler(contentError.Error);
            }
            return content.Profile.Fields;
        }
        #endregion

        #region Usergroups Group
        public Usergroup CreateUsergroup(string name, string channels = null, string description = null, string handle = null, bool include_count = true)
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            dict.Add("name", name);
            dict.Add("channels", channels);
            dict.Add("description", description);
            dict.Add("handle", handle);
            dict.Add("include_count", include_count.ToString());
            var request = QuerryBuilder(dict, "usergroups.create", Method.POST);
            var response = restClient.Execute(request);
            var content = JsonConvert.DeserializeObject<Usergroups.Info.RootObject>(response.Content);
            BaseError contentError;
            if (content.Ok == false)
            {
                contentError = JsonConvert.DeserializeObject<BaseError>(response.Content);
                ErrorHandler(contentError.Error);
            }
            return content.Usergroup;
        }

        public Usergroup DisableUsergroup(string usergroup, bool include_count = true)
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            dict.Add("usergroup", usergroup);
            dict.Add("include_count", include_count.ToString());
            var request = QuerryBuilder(dict, "usergroups.disable", Method.POST);
            var response = restClient.Execute(request);
            var content = JsonConvert.DeserializeObject<Usergroups.Info.RootObject>(response.Content);
            BaseError contentError;
            if (content.Ok == false)
            {
                contentError = JsonConvert.DeserializeObject<BaseError>(response.Content);
                ErrorHandler(contentError.Error);
            }
            return content.Usergroup;
        }

        public Usergroup EnableUsergroup(string usergroup, bool include_count = true)
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            dict.Add("usergroup", usergroup);
            dict.Add("include_count", include_count.ToString());
            var request = QuerryBuilder(dict, "usergroups.disable", Method.POST);
            var response = restClient.Execute(request);
            var content = JsonConvert.DeserializeObject<Usergroups.Info.RootObject>(response.Content);
            BaseError contentError;
            if (content.Ok == false)
            {
                contentError = JsonConvert.DeserializeObject<BaseError>(response.Content);
                ErrorHandler(contentError.Error);
            }
            return content.Usergroup;
        }

        public List<Usergroup> ListUsergroups(bool include_count = true, bool include_disable = true, bool include_users = true)
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            dict.Add("include_count", include_count.ToString());
            dict.Add("include_disable", include_disable.ToString());
            dict.Add("include_users", include_users.ToString());
            var request = QuerryBuilder(dict, "usergroups.disable", Method.GET);
            var response = restClient.Execute(request);
            var content = JsonConvert.DeserializeObject<Usergroups.List.RootObject>(response.Content);
            BaseError contentError;
            if (content.Ok == false)
            {
                contentError = JsonConvert.DeserializeObject<BaseError>(response.Content);
                ErrorHandler(contentError.Error);
            }
            return content.Usergroups;
        }

        public Usergroup UpdateUsergroup(string usergroup, string channels = null, string description = null, string handle = null, bool include_count = true, string name = null)
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            dict.Add("usergroup", usergroup);
            dict.Add("channels", channels);
            dict.Add("description", description);
            dict.Add("handle", handle);
            dict.Add("include_count", include_count.ToString());
            dict.Add("name", name);
            var request = QuerryBuilder(dict, "usergroups.update", Method.POST);
            var response = restClient.Execute(request);
            var content = JsonConvert.DeserializeObject<Usergroups.Info.RootObject>(response.Content);
            BaseError contentError;
            if (content.Ok == false)
            {
                contentError = JsonConvert.DeserializeObject<BaseError>(response.Content);
                ErrorHandler(contentError.Error);
            }
            return content.Usergroup;
        }

        public List<string> ListUsersInUsergroup(string usergroup, bool include_disabled = true)
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            dict.Add("usergroup", usergroup);
            dict.Add("include_disabled", include_disabled.ToString());
            var request = QuerryBuilder(dict, "usergroups.create", Method.GET);
            var response = restClient.Execute(request);
            var content = JsonConvert.DeserializeObject<Usergroups.Users.List.RootObject>(response.Content);
            BaseError contentError;
            if (content.Ok == false)
            {
                contentError = JsonConvert.DeserializeObject<BaseError>(response.Content);
                ErrorHandler(contentError.Error);
            }
            return content.Users;
        }

        public Usergroup UpdateUsersInUsergroup(string usergroup, List<string> users, bool include_disabled = true)
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            dict.Add("usergroup", usergroup);
            dict.Add("users", Extension.BuildString(users));
            dict.Add("include_disabled", include_disabled.ToString());
            var request = QuerryBuilder(dict, "usergroups.create", Method.POST);
            var response = restClient.Execute(request);
            var content = JsonConvert.DeserializeObject<Usergroups.Info.RootObject>(response.Content);
            BaseError contentError;
            if (content.Ok == false)
            {
                contentError = JsonConvert.DeserializeObject<BaseError>(response.Content);
                ErrorHandler(contentError.Error);
            }
            return content.Usergroup;
        }
        #endregion

        #region Users Group
        public List<User> ListUsers(string cursor = null, bool include_locale = false, int limit = 100, bool presence = false)
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            dict.Add("cursor", cursor);
            dict.Add("include_locale", include_locale.ToString());
            dict.Add("limit", limit.ToString());
            dict.Add("presence", presence.ToString());
            var request = QuerryBuilder(dict, "users.list", Method.GET);
            var response = restClient.Execute(request);
            var content = JsonConvert.DeserializeObject<Users.List.RootObject>(response.Content);
            BaseError contentError;
            if (content.Ok == false)
            {
                contentError = JsonConvert.DeserializeObject<BaseError>(response.Content);
                ErrorHandler(contentError.Error);
            }
            return content.Members;
        }

        public bool DeletePhoto()
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            var request = QuerryBuilder(dict, "users.deletePhoto", Method.GET);
            var response = restClient.Execute(request);
            var content = JsonConvert.DeserializeObject<BaseError>(response.Content);
            BaseError contentError;
            if (content.Ok == false)
            {
                contentError = JsonConvert.DeserializeObject<BaseError>(response.Content);
                ErrorHandler(contentError.Error);
            }
            return content.Ok;
        }

        public string GetPresence(string user)
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            dict.Add("user", user);
            var request = QuerryBuilder(dict, "users.getPresence", Method.GET);
            var response = restClient.Execute(request);
            var content = JsonConvert.DeserializeObject<Users.GetPresence.RootObject>(response.Content);
            BaseError contentError;
            if (content.Ok == false)
            {
                contentError = JsonConvert.DeserializeObject<BaseError>(response.Content);
                ErrorHandler(contentError.Error);
            }
            return content.Presence;
        }

        public UserIdentity Identity()
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            var request = QuerryBuilder(dict, "users.identity", Method.GET);
            var response = restClient.Execute(request);
            var content = JsonConvert.DeserializeObject<Users.RootObject>(response.Content);
            BaseError contentError;
            if (content.Ok == false)
            {
                contentError = JsonConvert.DeserializeObject<BaseError>(response.Content);
                ErrorHandler(contentError.Error);
            }
            return content.User;
        }

        public User UserInfo(string user, bool include_locale = false)
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            dict.Add("user", user);
            dict.Add("include_locale", include_locale.ToString());
            var request = QuerryBuilder(dict, "users.info", Method.GET);
            var response = restClient.Execute(request);
            var content = JsonConvert.DeserializeObject<Users.Info.RootObject>(response.Content);
            BaseError contentError;
            if (content.Ok == false)
            {
                contentError = JsonConvert.DeserializeObject<BaseError>(response.Content);
                ErrorHandler(contentError.Error);
            }
            return content.User;
        }

        public User LookupByEmailAddress(string email)
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            dict.Add("email", email);
            var request = QuerryBuilder(dict, "users.lookupByEmail", Method.GET);
            var response = restClient.Execute(request);
            var content = JsonConvert.DeserializeObject<Users.Info.RootObject>(response.Content);
            BaseError contentError;
            if (content.Ok == false)
            {
                contentError = JsonConvert.DeserializeObject<BaseError>(response.Content);
                ErrorHandler(contentError.Error);
            }
            return content.User;
        }

        public bool SetActive()
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            var request = QuerryBuilder(dict, "users.setActive", Method.POST);
            var response = restClient.Execute(request);
            var content = JsonConvert.DeserializeObject<BaseError>(response.Content);
            BaseError contentError;
            if (content.Ok == false)
            {
                contentError = JsonConvert.DeserializeObject<BaseError>(response.Content);
                ErrorHandler(contentError.Error);
            }
            return content.Ok;
        }

        public bool SetPresence(string presence)
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            dict.Add("presence", presence);
            var request = QuerryBuilder(dict, "users.setPresence", Method.POST);
            var response = restClient.Execute(request);
            var content = JsonConvert.DeserializeObject<BaseError>(response.Content);
            BaseError contentError;
            if (content.Ok == false)
            {
                contentError = JsonConvert.DeserializeObject<BaseError>(response.Content);
                ErrorHandler(contentError.Error);
            }
            return content.Ok;
        }

        public Dictionary<string, string> GetProfile(bool include_labels = true, string user = null)
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            dict.Add("include_labels", include_labels.ToString());
            dict.Add("user", user);
            var request = QuerryBuilder(dict, "users.profile.get", Method.GET);
            var response = restClient.Execute(request);
            var content = JsonConvert.DeserializeObject<Users.Profile.RootObject>(response.Content);
            BaseError contentError;
            if (content.Ok == false)
            {
                contentError = JsonConvert.DeserializeObject<BaseError>(response.Content);
                ErrorHandler(contentError.Error);
            }
            return content.Profile;
        }

        public Dictionary<string, string> SetProfile(string name = null, string profile = null, string user = null, string value = null)
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            dict.Add("name", name);
            dict.Add("profile", profile);
            dict.Add("user", user);
            dict.Add("value", value);
            var request = QuerryBuilder(dict, "users.profile.set", Method.POST);
            var response = restClient.Execute(request);
            var content = JsonConvert.DeserializeObject<Users.Profile.RootObject>(response.Content);
            BaseError contentError;
            if (content.Ok == false)
            {
                contentError = JsonConvert.DeserializeObject<BaseError>(response.Content);
                ErrorHandler(contentError.Error);
            }
            return content.Profile;
        }
        #endregion
    }
}
