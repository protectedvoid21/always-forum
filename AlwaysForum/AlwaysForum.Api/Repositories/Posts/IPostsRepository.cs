using AlwaysForum.Api.Models.Models;

namespace AlwaysForum.Api.Repositories.Posts;

public interface IPostsRepository
{
    Task<Post> GetAsync(int id);

    Task<IEnumerable<Post>> GetForSectionAsync(int id);

    Task AddAsync(Post post);

    Task<bool> IsAuthorAsync(int postId, string authorId);

    Task UpdateAsync(Post post);

    Task DeleteAsync(Post post);
}