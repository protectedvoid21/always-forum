using AlwaysForum.Api.Models.Models;

namespace AlwaysForum.Api.Repositories.Comments;

public interface ICommentsRepository
{
    Task AddAsync(string description, int postId, string authorId);

    Task<IEnumerable<Comment>> GetForPostAsync(int postId);

    Task<bool> IsAuthorAsync(int commentId, string authorId);

    Task<int> GetCountForPostAsync(int postId);

    Task UpdateAsync(int id, string description);

    Task DeleteAsync(int id);
}