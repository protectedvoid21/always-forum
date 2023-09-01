using AlwaysForum.Api.Database;
using AlwaysForum.Api.Models.Models;
using Microsoft.EntityFrameworkCore;

namespace AlwaysForum.Api.Services.CommentReports;

public class CommentReportsService : ICommentReportsService
{
    private readonly ForumDbContext _dbContext;

    public CommentReportsService(ForumDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddAsync(int commentId, string authorId, int reportTypeId, string description)
    {
        if (await _dbContext.CommentReports.FirstOrDefaultAsync(r =>
                r.AuthorId == authorId && r.CommentId == commentId) != null)
        {
            return;
        }

        CommentReport commentReport = new()
        {
            CommentId = commentId,
            Description = description,
            AuthorId = authorId,
            ReportTypeId = reportTypeId,
            CreateDate = DateTime.Now
        };

        await _dbContext.AddAsync(commentReport);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<IEnumerable<CommentReport>> GetAllAsync()
    {
        return await _dbContext.CommentReports
            .ToListAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var commentReport = await _dbContext.CommentReports.FindAsync(id);
        if (commentReport == null)
        {
            return;
        }

        _dbContext.Remove(commentReport);
        await _dbContext.SaveChangesAsync();
    }
}