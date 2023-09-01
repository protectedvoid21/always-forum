using AlwaysForum.Api.Models.Models;

namespace AlwaysForum.Api.Services.CommentReports;

public interface ICommentReportsService
{
    Task AddAsync(int commentId, string authorId, int reportTypeId, string description);

    Task<IEnumerable<CommentReport>> GetAllAsync();

    Task DeleteAsync(int id);
}