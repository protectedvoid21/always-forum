using AlwaysForum.Api.Models.Models;

namespace AlwaysForum.Api.Services.PostReports;

public interface IPostReportsService
{
    Task AddAsync(int postId, string authorId, int reportTypeId, string description);

    Task<IEnumerable<PostReport>> GetAllAsync();

    Task DeleteAsync(int id);
}