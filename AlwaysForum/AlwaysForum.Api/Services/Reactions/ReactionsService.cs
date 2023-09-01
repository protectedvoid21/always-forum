using AlwaysForum.Api.Database;
using AlwaysForum.Api.Models.Models;
using Microsoft.EntityFrameworkCore;

namespace AlwaysForum.Api.Services.Reactions;

public class ReactionsService : IReactionsService
{
    private readonly ForumDbContext _dbContext;

    public ReactionsService(ForumDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task ReactAsync(int postId, string userId, ReactionType reactionType)
    {
        var existingReaction =
            await _dbContext.Reactions.FirstOrDefaultAsync(r => r.UserId == userId && r.PostId == postId);
        if (existingReaction != null)
        {
            if (existingReaction.ReactionType == reactionType)
            {
                await RemoveAsync(existingReaction.Id);
                return;
            }

            await UpdateAsync(existingReaction.Id, reactionType);
            return;
        }

        Reaction reaction = new()
        {
            PostId = postId,
            UserId = userId,
            ReactionType = reactionType
        };

        await _dbContext.AddAsync(reaction);
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpVoteComment(int commentId, string userId, bool isUpVote) { }

    public async Task<IEnumerable<Reaction>> GetByPost(int postId)
    {
        return _dbContext.Reactions.Where(r => r.PostId == postId);
    }

    public async Task UpdateAsync(int id, ReactionType reactionType)
    {
        var reaction = await _dbContext.Reactions.FindAsync(id);
        if (reaction == null)
        {
            return;
        }

        reaction.ReactionType = reactionType;

        _dbContext.Reactions.Update(reaction);
        await _dbContext.SaveChangesAsync();
    }

    public async Task RemoveAsync(int id)
    {
        var reaction = await _dbContext.Reactions.FindAsync(id);
        if (reaction == null)
        {
            return;
        }

        _dbContext.Reactions.Remove(reaction);
        await _dbContext.SaveChangesAsync();
    }
}