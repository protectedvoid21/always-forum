using AlwaysForum.Api.Models.Models;

namespace AlwaysForum.Api.Repositories.Posts;

public interface IPostsRepository
{
    Task<Post?> GetAsync(int id, CancellationToken ct = default);

    Task<IEnumerable<Post>> GetForSectionAsync(int id, CancellationToken ct = default);

    Task AddAsync(Post post, CancellationToken ct = default);

    Task<bool> IsAuthorAsync(int postId, string authorId, CancellationToken ct = default);

    Task UpdateAsync(Post post, CancellationToken ct = default);

    Task DeleteAsync(Post post, CancellationToken ct = default);
}