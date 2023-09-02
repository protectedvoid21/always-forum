using AlwaysForum.Api.Models.Models;

namespace AlwaysForum.Api.Repositories.Comments;

public interface ICommentsRepository
{
    Task AddAsync(string description, int postId, string authorId);

    Task<IEnumerable<Comment>> GetByPost(int postId);

    Task<bool> IsAuthor(int commentId, string authorId);

    Task<int> GetCountInPost(int postId);

    Task UpdateAsync(int id, string description);

    Task DeleteAsync(int id);
}