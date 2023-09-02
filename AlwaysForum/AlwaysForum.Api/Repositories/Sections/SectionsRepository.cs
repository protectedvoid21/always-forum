using AlwaysForum.Api.Database;
using AlwaysForum.Api.Models.Models;
using Microsoft.EntityFrameworkCore;

namespace AlwaysForum.Api.Repositories.Sections;

public class SectionsRepository : ISectionsRepository
{
    private readonly ForumDbContext _dbContext;

    public SectionsRepository(ForumDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddAsync(string name, string description)
    {
        if (await _dbContext.Sections.AnyAsync(s => s.Name == name))
        {
            return;
        }

        Section section = new()
        {
            Name = name,
            Description = description
        };
        await _dbContext.AddAsync(section);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<Section> GetAsync(int id)
    {
        return await _dbContext.Sections
            .Where(s => s.Id == id)
            .FirstAsync();
    }

    public async Task<IEnumerable<Section>> GetAllAsync()
    {
        return await _dbContext.Sections.ToListAsync();
    }

    public async Task<int> GetPostCount(int id)
    {
        return await _dbContext.Posts.Where(p => p.SectionId == id).CountAsync();
    }

    public async Task UpdateAsync(int id, string name, string description)
    {
        var section = await _dbContext.Sections.FindAsync(id);
        section.Name = name;
        section.Description = description;

        _dbContext.Update(section);
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var section = await _dbContext.Sections
            .Include(s => s.Posts)
            .FirstAsync(s => s.Id == id);

        _dbContext.Remove(section);
        await _dbContext.SaveChangesAsync();
    }
}