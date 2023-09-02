using AlwaysForum.Api.Database;
using AlwaysForum.Api.Models.Models;
using Microsoft.EntityFrameworkCore;

namespace AlwaysForum.Api.Repositories.PostReports;

public class PostReportsRepository : IPostReportsRepository
{
    private readonly ForumDbContext _dbContext;

    public PostReportsRepository(ForumDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddAsync(int postId, string authorId, int reportTypeId, string description)
    {
        if (await _dbContext.PostReports.FirstOrDefaultAsync(r => r.AuthorId == authorId && r.PostId == postId) != null)
        {
            return;
        }

        PostReport postReport = new()
        {
            PostId = postId,
            Description = description,
            AuthorId = authorId,
            ReportTypeId = reportTypeId,
            CreateDate = DateTime.Now
        };

        await _dbContext.AddAsync(postReport);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<IEnumerable<PostReport>> GetAllAsync()
    {
        return await _dbContext.PostReports
            .ToListAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var postReport = await _dbContext.PostReports.FindAsync(id);
        if (postReport == null)
        {
            return;
        }

        _dbContext.Remove(postReport);
        await _dbContext.SaveChangesAsync();
    }
}