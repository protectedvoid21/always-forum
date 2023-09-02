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

    public async Task AddAsync(string description, int postId, string authorId)
    {
        Comment comment = new()
        {
            Description = description,
            PostId = postId,
            AuthorId = authorId,
            CreatedTime = DateTime.Now
        };
        await _dbContext.AddAsync(comment);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<IEnumerable<Comment>> GetByPost(int postId)
    {
        return _dbContext.Comments
            .Include(c => c.Author)
            .Where(c => c.PostId == postId);
    }

    public async Task<bool> IsAuthor(int commentId, string authorId)
    {
        var comment = await _dbContext.Comments.FindAsync(commentId);
        if (comment == null)
        {
            return false;
        }

        return comment.AuthorId == authorId;
    }

    public async Task<int> GetCountInPost(int postId)
    {
        return await _dbContext.Comments.CountAsync(p => p.PostId == postId);
    }

    public async Task UpdateAsync(int id, string description)
    {
        var comment = await _dbContext.Comments.FindAsync(id);
        if (comment == null)
        {
            return;
        }

        comment.Description = description;

        _dbContext.Update(comment);
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var comment = await _dbContext.Comments
            .Include(c => c.CommentReports)
            .FirstOrDefaultAsync(c => c.Id == id);
        if (comment == null)
        {
            return;
        }

        _dbContext.Remove(comment);
        await _dbContext.SaveChangesAsync();
    }
}