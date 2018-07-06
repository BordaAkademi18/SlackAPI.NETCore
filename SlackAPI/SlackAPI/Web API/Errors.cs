using System;
using System.Collections.Generic;
using System.Text;

namespace SlackAPI
{
    public class AccountInactiveException : Exception
    {
        public AccountInactiveException(string message) : base(message)
        {
        }
    }

    public class AlreadyAchievedException : Exception
    {
        public AlreadyAchievedException(string message) : base(message)
        {
        }
    }

    public class AlreadyInChannelException : Exception
    {
        public AlreadyInChannelException(string message) : base(message)
        {
        }
    }

    public class AlreadyPinnedException : Exception
    {
        public AlreadyPinnedException(string message) : base(message)
        {
        }
    }

    public class AlreadyReactedException : Exception
    {
        public AlreadyReactedException(string message) : base(message)
        {
        }
    }

    public class AsUserNotSupportedException : Exception
    {
        public AsUserNotSupportedException(string message) : base(message)
        {
        }
    }

    public class BadImageException : Exception
    {
        public BadImageException(string message) : base(message)
        {
        }
    }

    public class BadTimestampException : Exception
    {
        public BadTimestampException(string message) : base(message)
        {
        }
    }

    public class CannotAddBotException : Exception
    {
        public CannotAddBotException(string message) : base(message)
        {
        }
    }

    public class CannotAddOthersException : Exception
    {
        public CannotAddOthersException(string message) : base(message)
        {
        }
    }

    public class CannotAddOthersRecurringException : Exception
    {
        public CannotAddOthersRecurringException(string message) : base(message)
        {
        }
    }

    public class CannotAddSlackbotException : Exception
    {
        public CannotAddSlackbotException(string message) : base(message)
        {
        }
    }

    public class CannotCompleteOthersException : Exception
    {
        public CannotCompleteOthersException(string message) : base(message)
        {
        }
    }

    public class CannotCompleteRecurringException : Exception
    {
        public CannotCompleteRecurringException(string message) : base(message)
        {
        }
    }

    public class CannotFindServiceException : Exception
    {
        public CannotFindServiceException(string message) : base(message)
        {
        }
    }

    public class CannotParseException : Exception
    {
        public CannotParseException(string message) : base(message)
        {
        }
    }

    public class CannotPromptException : Exception
    {
        public CannotPromptException(string message) : base(message)
        {
        }
    }

    public class CannotUnfurlUrlException : Exception
    {
        public CannotUnfurlUrlException(string message) : base(message)
        {
        }
    }

    public class CannotUpdateAdminUserException : Exception
    {
        public CannotUpdateAdminUserException(string message) : base(message)
        {
        }
    }

    public class CantAchieveGeneralException : Exception
    {
        public CantAchieveGeneralException(string message) : base(message)
        {
        }
    }

    public class CantDeleteException : Exception
    {
        public CantDeleteException(string message) : base(message)
        {
        }
    }

    public class CantDeleteFileException : Exception
    {
        public CantDeleteFileException(string message) : base(message)
        {
        }
    }

    public class CantDeleteMessageException : Exception
    {
        public CantDeleteMessageException(string message) : base(message)
        {
        }
    }

    public class CantEditException : Exception
    {
        public CantEditException(string message) : base(message)
        {
        }
    }

    public class CantInviteException : Exception
    {
        public CantInviteException(string message) : base(message)
        {
        }
    }

    public class CantInviteSelfException : Exception
    {
        public CantInviteSelfException(string message) : base(message)
        {
        }
    }

    public class CantKickFromGeneralException : Exception
    {
        public CantKickFromGeneralException(string message) : base(message)
        {
        }
    }

    public class CantKickSelfException : Exception
    {
        public CantKickSelfException(string message) : base(message)
        {
        }
    }

    public class CantLeaveGeneralException : Exception
    {
        public CantLeaveGeneralException(string message) : base(message)
        {
        }
    }

    public class CantUpdateMessageException : Exception
    {
        public CantUpdateMessageException(string message) : base(message)
        {
        }
    }

    public class ChannelNotFoundException : Exception
    {
        public ChannelNotFoundException(string message) : base(message)
        {
        }
    }

    public class ComplianceExportsPreventDeletionException : Exception
    {
        public ComplianceExportsPreventDeletionException(string message) : base(message)
        {
        }
    }

    public class EditWindowClosedException : Exception
    {
        public EditWindowClosedException(string message) : base(message)
        {
        }
    }

    public class FatalErrorException : Exception
    {
        public FatalErrorException(string message) : base(message)
        {
        }
    }

    public class FetchMembersFailedException : Exception
    {
        public FetchMembersFailedException(string message) : base(message)
        {
        }
    }

    public class FileCommentNotFoundException : Exception
    {
        public FileCommentNotFoundException(string message) : base(message)
        {
        }
    }

    public class FileDeletedException : Exception
    {
        public FileDeletedException(string message) : base(message)
        {
        }
    }

    public class FileNotFoundException : Exception
    {
        public FileNotFoundException(string message) : base(message)
        {
        }
    }

    public class FileNotSharedException : Exception
    {
        public FileNotSharedException(string message) : base(message)
        {
        }
    }

    public class InvalidArgNameException : Exception
    {
        public InvalidArgNameException(string message) : base(message)
        {
        }
    }

    public class InvalidArrayArgException : Exception
    {
        public InvalidArrayArgException(string message) : base(message)
        {
        }
    }

    public class InvalidAuthException : Exception
    {
        public InvalidAuthException(string message) : base(message)
        {
        }
    }

    public class InvalidChannelException : Exception
    {
        public InvalidChannelException(string message) : base(message)
        {
        }
    }

    public class InvalidCharsetException : Exception
    {
        public InvalidCharsetException(string message) : base(message)
        {
        }
    }

    public class InvalidCursorException : Exception
    {
        public InvalidCursorException(string message) : base(message)
        {
        }
    }

    public class InvalidFormDataException : Exception
    {
        public InvalidFormDataException(string message) : base(message)
        {
        }
    }

    public class InvalidLimitException : Exception
    {
        public InvalidLimitException(string message) : base(message)
        {
        }
    }

    public class InvalidNameException : Exception
    {
        public InvalidNameException(string message) : base(message)
        {
        }
    }

    public class InvalidNameMaxlengthException : Exception
    {
        public InvalidNameMaxlengthException(string message) : base(message)
        {
        }
    }

    public class InvalidNamePunctuationException : Exception
    {
        public InvalidNamePunctuationException(string message) : base(message)
        {
        }
    }

    public class InvalidNameRequiredException : Exception
    {
        public InvalidNameRequiredException(string message) : base(message)
        {
        }
    }

    public class InvalidNameSpecialsException : Exception
    {
        public InvalidNameSpecialsException(string message) : base(message)
        {
        }
    }

    public class InvalidPostTypeException : Exception
    {
        public InvalidPostTypeException(string message) : base(message)
        {
        }
    }

    public class InvalidPresenceException : Exception
    {
        public InvalidPresenceException(string message) : base(message)
        {
        }
    }

    public class InvalidProfileException : Exception
    {
        public InvalidProfileException(string message) : base(message)
        {
        }
    }

    public class InvalidTsLatestException : Exception
    {
        public InvalidTsLatestException(string message) : base(message)
        {
        }
    }

    public class InvalidTsOldestException : Exception
    {
        public InvalidTsOldestException(string message) : base(message)
        {
        }
    }

    public class InvalidTypesException : Exception
    {
        public InvalidTypesException(string message) : base(message)
        {
        }
    }

    public class InvalidUsersExceptiom : Exception
    {
        public InvalidUsersExceptiom(string message) : base(message)
        {
        }
    }

    public class IsArchivedException : Exception
    {
        public IsArchivedException(string message) : base(message)
        {
        }
    }

    public class MessageNotFoundException : Exception
    {
        public MessageNotFoundException(string message) : base(message)
        {
        }
    }

    public class MethodNotSupportedForChannelTypeException : Exception
    {
        public MethodNotSupportedForChannelTypeException(string message) : base(message)
        {
        }
    }

    public class MissingDurationException : Exception
    {
        public MissingDurationException(string message) : base(message)
        {
        }
    }

    public class MissingPostTypeException : Exception
    {
        public MissingPostTypeException(string message) : base(message)
        {
        }
    }

    public class MissingScopeException : Exception
    {
        public MissingScopeException(string message) : base(message)
        {
        }
    }

    public class MissingUnfurlsException : Exception
    {
        public MissingUnfurlsException(string message) : base(message)
        {
        }
    }

    public class MessageTooLongException : Exception
    {
        public MessageTooLongException(string message) : base(message)
        {
        }
    }

    public class NameTakenException : Exception
    {
        public NameTakenException(string message) : base(message)
        {
        }
    }

    public class NoChannelException : Exception
    {
        public NoChannelException(string message) : base(message)
        {
        }
    }

    public class NoCommentException : Exception
    {
        public NoCommentException(string message) : base(message)
        {
        }
    }

    public class NoItemSpecifiedException : Exception
    {
        public NoItemSpecifiedException(string message) : base(message)
        {
        }
    }

    public class NoPermissionException : Exception
    {
        public NoPermissionException(string message) : base(message)
        {
        }
    }

    public class NoReactionException : Exception
    {
        public NoReactionException(string message) : base(message)
        {
        }
    }

    public class NoTextException : Exception
    {
        public NoTextException(string message) : base(message)
        {
        }
    }

    public class NotAdminException : Exception
    {
        public NotAdminException(string message) : base(message)
        {
        }
    }

    public class NotAllowedException : Exception
    {
        public NotAllowedException(string message) : base(message)
        {
        }
    }

    public class NotApplicationAdminException : Exception
    {
        public NotApplicationAdminException(string message) : base(message)
        {
        }
    }

    public class NotAuthedException : Exception
    {
        public NotAuthedException(string message) : base(message)
        {
        }
    }

    public class NotAuthorizedException : Exception
    {
        public NotAuthorizedException(string message) : base(message)
        {
        }
    }

    public class NotEnoughUsersException : Exception
    {
        public NotEnoughUsersException(string message) : base(message)
        {
        }
    }

    public class NotFoundException : Exception
    {
        public NotFoundException(string message) : base(message)
        {
        }
    }

    public class NotInChannelException : Exception
    {
        public NotInChannelException(string message) : base(message)
        {
        }
    }

    public class NotPinnableException : Exception
    {
        public NotPinnableException(string message) : base(message)
        {
        }
    }

    public class NotPinnedException : Exception
    {
        public NotPinnedException(string message) : base(message)
        {
        }
    }

    public class OrgLoginRequiredException : Exception
    {
        public OrgLoginRequiredException(string message) : base(message)
        {
        }
    }

    public class PaginationNotAvailableException : Exception
    {
        public PaginationNotAvailableException(string message) : base(message)
        {
        }
    }

    public class PermissionDeniedException : Exception
    {
        public PermissionDeniedException(string message) : base(message)
        {
        }
    }

    public class PostingToGeneralChannelDeniedException : Exception
    {
        public PostingToGeneralChannelDeniedException(string message) : base(message)
        {
        }
    }

    public class ProfileSetFailedException : Exception
    {
        public ProfileSetFailedException(string message) : base(message)
        {
        }
    }

    public class RateLimitedException : Exception
    {
        public RateLimitedException(string message) : base(message)
        {
        }
    }

    public class RequestTimeoutException : Exception
    {
        public RequestTimeoutException(string message) : base(message)
        {
        }
    }

    public class ReservedNameException : Exception
    {
        public ReservedNameException(string message) : base(message)
        {
        }
    }

    public class RestrictedActionException : Exception
    {
        public RestrictedActionException(string message) : base(message)
        {
        }
    }

    public class RestrictedActionNonThreadableChannelException : Exception
    {
        public RestrictedActionNonThreadableChannelException(string message) : base(message)
        {
        }
    }

    public class RestrictedActionReadonlyChannelException : Exception
    {
        public RestrictedActionReadonlyChannelException(string message) : base(message)
        {
        }
    }

    public class RestrictedActionThreadonlyChannelException : Exception
    {
        public RestrictedActionThreadonlyChannelException(string message) : base(message)
        {
        }
    }

    public class SnoozeEndFailedException : Exception
    {
        public SnoozeEndFailedException(string message) : base(message)
        {
        }
    }

    public class SnoozeFailedException : Exception
    {
        public SnoozeFailedException(string message) : base(message)
        {
        }
    }

    public class SnoozeNotActiveException : Exception
    {
        public SnoozeNotActiveException(string message) : base(message)
        {
        }
    }

    public class TeamAddedToOrgException : Exception
    {
        public TeamAddedToOrgException(string message) : base(message)
        {
        }
    }

    public class ThreadNotFoundException : Exception
    {
        public ThreadNotFoundException(string message) : base(message)
        {
        }
    }

    public class TokenRevokedException : Exception
    {
        public TokenRevokedException(string message) : base(message)
        {
        }
    }

    public class TooLargeImageException : Exception
    {
        public TooLargeImageException(string message) : base(message)
        {
        }
    }

    public class TooLongTextException : Exception
    {
        public TooLongTextException(string message) : base(message)
        {
        }
    }

    public class TooManyAttachmentsException : Exception
    {
        public TooManyAttachmentsException(string message) : base(message)
        {
        }
    }

    public class TooManyConvosForAppOnTeamException : Exception
    {
        public TooManyConvosForAppOnTeamException(string message) : base(message)
        {
        }
    }

    public class TooManyConvosForTeamException : Exception
    {
        public TooManyConvosForTeamException(string message) : base(message)
        {
        }
    }

    public class TooManyEmoji : Exception
    {
        public TooManyEmoji(string message) : base(message)
        {
        }
    }

    public class TooManyFrames : Exception
    {
        public TooManyFrames(string message) : base(message)
        {
        }
    }

    public class TooManyReactions : Exception
    {
        public TooManyReactions(string message) : base(message)
        {
        }
    }

    public class TooManyUsers : Exception
    {
        public TooManyUsers(string message) : base(message)
        {
        }
    }

    public class UnknownErrorException : Exception
    {
        public UnknownErrorException(string message) : base(message)
        {
        }
    }

    public class UnknownTypeException : Exception
    {
        public UnknownTypeException(string message) : base(message)
        {
        }
    }

    public class URAMaxChannelsException : Exception
    {
        public URAMaxChannelsException(string message) : base(message)
        {
        }
    }

    public class UserDisabledException : Exception
    {
        public UserDisabledException(string message) : base(message)
        {
        }
    }

    public class UserDoesNotOwnChannelException : Exception
    {
        public UserDoesNotOwnChannelException(string message) : base(message)
        {
        }
    }

    public class UserIsBotException : Exception
    {
        public UserIsBotException(string message) : base(message)
        {
        }
    }

    public class UserIsRestrictedException : Exception
    {
        public UserIsRestrictedException(string message) : base(message)
        {
        }
    }

    public class UserIsUltraRestrictedException : Exception
    {
        public UserIsUltraRestrictedException(string message) : base(message)
        {
        }
    }

    public class UserNotFoundException : Exception
    {
        public UserNotFoundException(string message) : base(message)
        {
        }
    }

    public class UserNotInChannelException : Exception
    {
        public UserNotInChannelException(string message) : base(message)
        {
        }
    }

    public class UserNotVisibleException : Exception
    {
        public UserNotVisibleException(string message) : base(message)
        {
        }
    }

    public class UsersListNotSuppliedException : Exception
    {
        public UsersListNotSuppliedException(string message) : base(message)
        {
        }
    }

    public class UsersNotFoundException : Exception
    {
        public UsersNotFoundException(string message) : base(message)
        {
        }
    }
}
