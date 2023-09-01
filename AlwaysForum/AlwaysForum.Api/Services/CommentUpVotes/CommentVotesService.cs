using AlwaysForum.Api.Database;
using AlwaysForum.Api.Models.Models;
using Microsoft.EntityFrameworkCore;

namespace AlwaysForum.Api.Services.CommentUpVotes;

public class CommentVotesService : ICommentVotesService
{
    private readonly ForumDbContext _dbContext;

    public CommentVotesService(ForumDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task VoteAsync(int commentId, string userId, bool isUpVote)
    {
        var vote = await _dbContext.CommentUpVotes.FirstOrDefaultAsync(c =>
            c.CommentId == commentId && c.AuthorId == userId);
        if (vote != null)
        {
            if (vote.IsUpVote == isUpVote)
            {
                _dbContext.Remove(vote);
                await _dbContext.SaveChangesAsync();
                return;
            }

            vote.IsUpVote = isUpVote;
            _dbContext.Update(vote);
            await _dbContext.SaveChangesAsync();
            return;
        }

        vote = new CommentVote
        {
            CommentId = commentId,
            AuthorId = userId,
            IsUpVote = isUpVote
        };

        await _dbContext.AddAsync(vote);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<CommentVoteStatus> IsVotedByUser(int commentId, string userId)
    {
        var commentVote =
            await _dbContext.CommentUpVotes.FirstOrDefaultAsync(c => c.AuthorId == userId && c.CommentId == commentId);
        if (commentVote == null)
        {
            return CommentVoteStatus.None;
        }

        return commentVote.IsUpVote ? CommentVoteStatus.UpVoted : CommentVoteStatus.DownVoted;
    }

    public async Task<int> GetVoteCount(int commentId)
    {
        IEnumerable<CommentVote> votes = _dbContext.CommentUpVotes.Where(c => c.CommentId == commentId);

        return votes.Count(v => v.IsUpVote) - votes.Count(v => v.IsUpVote == false);
    }
}