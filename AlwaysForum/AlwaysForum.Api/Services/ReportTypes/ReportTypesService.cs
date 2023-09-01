using AlwaysForum.Api.Database;
using AlwaysForum.Api.Models.Models;

namespace AlwaysForum.Api.Services.ReportTypes;

public class ReportTypesService : IReportTypesService
{
    private readonly ForumDbContext _dbContext;

    public ReportTypesService(ForumDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddAsync(string name)
    {
        ReportType reportType = new()
        {
            Name = name
        };

        await _dbContext.AddAsync(reportType);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<ReportType> GetAsync(int id)
    {
        return await _dbContext.ReportTypes.FindAsync(id);
    }

    public async Task<IEnumerable<ReportType>> GetAllAsync()
    {
        return _dbContext.ReportTypes;
    }

    public async Task UpdateAsync(int id, string name)
    {
        var reportType = await _dbContext.ReportTypes.FindAsync(id);
        if (reportType == null)
        {
            return;
        }

        reportType.Name = name;
        _dbContext.Update(reportType);
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var reportType = await _dbContext.ReportTypes.FindAsync(id);
        if (reportType == null)
        {
            return;
        }

        _dbContext.Remove(reportType);
        await _dbContext.SaveChangesAsync();
    }
}