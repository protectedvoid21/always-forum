using AlwaysForum.Api.Models.Models;

namespace AlwaysForum.Api.Repositories.Sections;

public interface ISectionsRepository
{
    Task AddAsync(string name, string description);

    Task<Section> GetAsync(int id);

    Task<IEnumerable<Section>> GetAllAsync();

    Task<int> GetPostCount(int id);

    Task UpdateAsync(int id, string name, string description);

    Task DeleteAsync(int id);
}