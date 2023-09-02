using AlwaysForum.Api.Models.Models;

namespace AlwaysForum.Api.Repositories.PostReports;

public interface IPostReportsRepository
{
    Task AddAsync(int postId, string authorId, int reportTypeId, string description);

    Task<IEnumerable<PostReport>> GetAllAsync();

    Task DeleteAsync(int id);
}