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

    public async Task<Post> GetById(int id)
    {
        var post = await _dbContext.Posts
            .Include(p => p.Author)
            .Include(p => p.Tags)
            .FirstAsync(p => p.Id == id);
        return post;
    }

    public async Task<IEnumerable<Post>> GetBySection(int id)
    {
        return await _dbContext.Posts
            .Include(p => p.Author)
            .Where(p => p.SectionId == id)
            .ToListAsync();
    }

    public async Task<int> GetCommentCount(int id)
    {
        return await _dbContext.Comments.Where(c => c.PostId == id).CountAsync();
    }

    public async Task<int> AddAsync(string title, string description, string authorId, int sectionId)
    {
        Post post = new()
        {
            Title = title,
            Description = description,
            AuthorId = authorId,
            SectionId = sectionId,
            CreatedDate = DateTime.Now
        };

        await _dbContext.AddAsync(post);
        await _dbContext.SaveChangesAsync();

        return post.Id;
    }

    public async Task<bool> IsAuthor(int postId, string authorId)
    {
        var post = await _dbContext.Posts.FindAsync(postId);
        if (post == null)
        {
            return false;
        }

        return post.AuthorId == authorId;
    }

    public async Task UpdateAsync(int id, string title, string description)
    {
        var post = await _dbContext.Posts.FindAsync(id);
        if (post == null)
        {
            return;
        }

        post.Title = title;
        post.Description = description;

        _dbContext.Update(post);
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var post = await _dbContext.Posts
            .Include(p => p.Comments)
            .Include(p => p.PostReports)
            .FirstOrDefaultAsync(p => p.Id == id);
        if (post == null)
        {
            return;
        }

        _dbContext.Remove(post);
        await _dbContext.SaveChangesAsync();
    }
}