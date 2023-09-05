using AlwaysForum.Api.Constants;
using AlwaysForum.Api.Database;
using AlwaysForum.Api.Models.Models;
using AlwaysForum.Api.Repositories.Tags;
using Microsoft.EntityFrameworkCore;

namespace AlwaysForum.Api.Repositories.Posts;

public class PostsRepository : IPostsRepository
{
    private readonly ForumDbContext _dbContext;
    
    public PostsRepository(ForumDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Post?> GetAsync(int id, CancellationToken ct = default)
    {
        return await _dbContext.Posts
            .Include(p => p.Author)
            .Include(p => p.Tags)
            .FirstOrDefaultAsync(p => p.Id == id, ct);
    }

    public async Task<IEnumerable<Post>> GetForSectionAsync(int id, CancellationToken ct = default)
    {
        return await _dbContext.Posts
            .Include(p => p.Author)
            .Where(p => p.SectionId == id)
            .ToListAsync(ct);
    }

    public async Task AddAsync(Post post, CancellationToken ct = default)
    {
        _dbContext.Add(post);
        await _dbContext.SaveChangesAsync(ct);
    }

    public async Task<bool> IsAuthorAsync(int postId, string authorId, CancellationToken ct = default)
    {
        var post = await _dbContext.Posts.FindAsync(new object?[] { postId }, cancellationToken: ct);
        if (post is null)
        {
            return false;
        }

        return post.AuthorId == authorId;
    }

    public async Task UpdateAsync(Post post, CancellationToken ct = default)
    {
        _dbContext.Update(post);
        await _dbContext.SaveChangesAsync(ct);
    }

    public async Task DeleteAsync(Post post, CancellationToken ct = default)
    {
        _dbContext.Remove(post);
        await _dbContext.SaveChangesAsync(ct);
    }
}