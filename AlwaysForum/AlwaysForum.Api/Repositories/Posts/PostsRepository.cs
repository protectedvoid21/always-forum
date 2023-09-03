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

    public async Task<Post> GetAsync(int id)
    {
        var post = await _dbContext.Posts
            .Include(p => p.Author)
            .Include(p => p.Tags)
            .FirstAsync(p => p.Id == id);
        return post;
    }

    public async Task<IEnumerable<Post>> GetForSectionAsync(int id)
    {
        return await _dbContext.Posts
            .Include(p => p.Author)
            .Where(p => p.SectionId == id)
            .ToListAsync();
    }

    public async Task AddAsync(Post post)
    {
        _dbContext.Add(post);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<bool> IsAuthorAsync(int postId, string authorId)
    {
        var post = await _dbContext.Posts.FindAsync(postId);
        if (post is null)
        {
            return false;
        }

        return post.AuthorId == authorId;
    }

    public async Task UpdateAsync(Post post)
    {
        _dbContext.Update(post);
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(Post post)
    {
        _dbContext.Remove(post);
        await _dbContext.SaveChangesAsync();
    }
}