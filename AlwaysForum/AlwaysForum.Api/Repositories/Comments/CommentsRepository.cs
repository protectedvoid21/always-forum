using AlwaysForum.Api.Database;
using AlwaysForum.Api.Models.Models;
using Microsoft.EntityFrameworkCore;

namespace AlwaysForum.Api.Repositories.Comments;

public class CommentsRepository : ICommentsRepository
{
    private readonly ForumDbContext _dbContext;

    public CommentsRepository(ForumDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddAsync(Comment comment, CancellationToken ct = default)
    {
        _dbContext.Add(comment);
        await _dbContext.SaveChangesAsync(ct);
    }

    public async Task<IEnumerable<Comment>> GetForPostAsync(int postId, CancellationToken ct = default)
    {
        return await _dbContext.Comments
            .Include(c => c.Author)
            .Where(c => c.PostId == postId)
            .ToListAsync(ct);
    }

    public async Task<bool> IsAuthorAsync(int commentId, string authorId, CancellationToken ct = default)
    {
        var comment = await _dbContext.Comments.FindAsync(commentId);
        if (comment == null)
        {
            return false;
        }

        return comment.AuthorId == authorId;
    }

    public async Task<int> GetCountForPostAsync(int postId, CancellationToken ct = default)
    {
        return await _dbContext.Comments.CountAsync(p => p.PostId == postId, ct);
    }

    public async Task UpdateAsync(int id, string description, CancellationToken ct = default)
    {
        var comment = await _dbContext.Comments.FindAsync(id);
        if (comment == null)
        {
            return;
        }

        comment.Description = description;

        _dbContext.Update(comment);
        await _dbContext.SaveChangesAsync(ct);
    }

    public async Task DeleteAsync(int id, CancellationToken ct = default)
    {
        var comment = await _dbContext.Comments
            .Include(c => c.CommentReports)
            .FirstOrDefaultAsync(c => c.Id == id, ct);
        if (comment == null)
        {
            return;
        }

        _dbContext.Remove(comment);
        await _dbContext.SaveChangesAsync(ct);
    }
}