using AlwaysForum.Api.Models.Models;

namespace AlwaysForum.Api.Repositories.CommentReports;

public interface ICommentReportsRepository
{
    Task AddAsync(int commentId, string authorId, int reportTypeId, string description);

    Task<IEnumerable<CommentReport>> GetAllAsync();

    Task DeleteAsync(int id);
}