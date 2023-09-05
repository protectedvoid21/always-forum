using AlwaysForum.Api.Models.Api.Sections;

namespace AlwaysForum.Api.Services.Sections;

public interface ISectionsService
{
    Task<GetAllSectionResponse> GetAllAsync(CancellationToken ct = default);
}