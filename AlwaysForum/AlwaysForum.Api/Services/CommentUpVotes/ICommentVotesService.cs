using AlwaysForum.Api.Models.Models;

namespace AlwaysForum.Api.Services.CommentUpVotes;

public interface ICommentVotesService
{
    Task VoteAsync(int commentId, string userId, bool isUpVote);

    Task<CommentVoteStatus> IsVotedByUser(int commentId, string userId);

    Task<int> GetVoteCount(int commentId);
}