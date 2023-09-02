using AlwaysForum.Api.Models.Models;

namespace AlwaysForum.Api.Repositories.Tags;

public interface ITagsRepository
{
    Task AddAsync(string name);

    Task AddToPost(int tagId, int postId);

    Task UpdateAsync(int id, string name);

    Task UpdateTagsOnPost(int postId, IEnumerable<int> tagIds);

    Task<Tag> GetById(int id);

    Task<IEnumerable<Tag>> GetAllAsync();

    Task<IEnumerable<Tag>> GetTrendingForSection(int sectionId, int count);

    Task DeleteAsync(int id);
}