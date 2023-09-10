using AlwaysForum.Api.Models.Models;

namespace AlwaysForum.Api.Repositories.Comments;

public interface ICommentsRepository
{
    Task AddAsync(Comment comment, CancellationToken ct = default);

    Task<IEnumerable<Comment>> GetForPostAsync(int postId, CancellationToken ct = default);

    Task<bool> IsAuthorAsync(int commentId, string authorId, CancellationToken ct = default);

    Task<int> GetCountForPostAsync(int postId, CancellationToken ct = default);

    Task UpdateAsync(int id, string description, CancellationToken ct = default);

    Task DeleteAsync(int id, CancellationToken ct = default);
}