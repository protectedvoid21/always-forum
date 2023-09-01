using AlwaysForum.Api.Constants;
using AlwaysForum.Api.Database;
using AlwaysForum.Api.Models.Models;
using AlwaysForum.Api.Services.Tags;
using Microsoft.EntityFrameworkCore;

namespace AlwaysForum.Api.Services.Posts;

public class PostsService : IPostsService
{
    private readonly ForumDbContext _dbContext;
    private readonly ITagsRepository _tagsRepository;

    public PostsService(ForumDbContext dbContext, ITagsRepository tagsRepository)
    {
        _dbContext = dbContext;
        _tagsRepository = tagsRepository;
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
        return _dbContext.Posts
            .Include(p => p.Author)
            .Where(p => p.SectionId == id);
    }

    public async Task<int> GetCommentCount(int id)
    {
        return await _dbContext.Comments.Where(c => c.PostId == id).CountAsync();
    }

    public async Task<int> AddAsync(string title, string description, string authorId, int sectionId,
        IEnumerable<int> tagIds)
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

        tagIds = tagIds.Take(GlobalConstants.MaxTagsOnPost);
        foreach (var tagId in tagIds)
        {
            await _tagsRepository.AddToPost(tagId, post.Id);
        }

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

    public async Task UpdateAsync(int id, string title, string description, IEnumerable<int> tagIds)
    {
        var post = await _dbContext.Posts.FindAsync(id);
        if (post == null)
        {
            return;
        }

        post.Title = title;
        post.Description = description;

        tagIds = tagIds.Take(GlobalConstants.MaxTagsOnPost);
        await _tagsRepository.UpdateTagsOnPost(id, tagIds);

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