using AlwaysForum.Api.Database;
using AlwaysForum.Api.Models.Models;
using Microsoft.EntityFrameworkCore;

namespace AlwaysForum.Api.Repositories.Tags;

public class TagsRepository : ITagsRepository
{
    private readonly ForumDbContext _dbContext;

    public TagsRepository(ForumDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddAsync(string name)
    {
        if (await _dbContext.Tags.FirstOrDefaultAsync(t => t.Name == name) != null)
        {
            return;
        }

        Tag tag = new()
        {
            Name = name
        };

        await _dbContext.AddAsync(tag);
        await _dbContext.SaveChangesAsync();
    }

    public async Task AddToPost(int tagId, int postId)
    {
        PostTag postTag = new()
        {
            PostId = postId,
            TagId = tagId
        };

        await _dbContext.AddAsync(postTag);
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(int id, string name)
    {
        var tag = await _dbContext.Tags.FindAsync(id);
        if (tag == null)
        {
            return;
        }

        tag.Name = name;
        _dbContext.Update(tag);
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateTagsOnPost(int postId, IEnumerable<int> tagIds)
    {
        IEnumerable<PostTag> existingPostTags = _dbContext.PostTags.Where(pt => pt.PostId == postId);
        _dbContext.RemoveRange(existingPostTags);

        List<PostTag> postTags = new();
        foreach (var tagId in tagIds)
        {
            postTags.Add(new PostTag
            {
                PostId = postId,
                TagId = tagId
            });
        }

        await _dbContext.AddRangeAsync(postTags);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<Tag> GetById(int id)
    {
        return await _dbContext.Tags.FindAsync(id);
    }

    public async Task<IEnumerable<Tag>> GetAllAsync()
    {
        return _dbContext.Tags;
    }

    public async Task<IEnumerable<Tag>> GetTrendingForSection(int sectionId, int count)
    {
        return _dbContext.Posts
            .Where(p => p.SectionId == sectionId)
            .SelectMany(p => p.Tags)
            .Distinct()
            .OrderByDescending(t => t.Posts.Count)
            .Where(t => t.Posts.Count > 0)
            .Take(count);
    }

    public async Task DeleteAsync(int id)
    {
        var tag = await _dbContext.Tags.FindAsync(id);
        if (tag == null)
        {
            return;
        }

        _dbContext.Remove(tag);
        await _dbContext.SaveChangesAsync();
    }
}