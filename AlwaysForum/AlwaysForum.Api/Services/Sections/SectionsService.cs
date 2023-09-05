using AlwaysForum.Api.Models.Api.Sections;
using AlwaysForum.Api.Repositories.Sections;
using AlwaysForum.Api.Utils;
using AlwaysForum.Api.Utils.Mappings;

namespace AlwaysForum.Api.Services.Sections;

public class SectionsService : ServiceBase, ISectionsService
{
    private readonly ISectionsRepository _sectionsRepository;
    
    public SectionsService(ISectionsRepository sectionsRepository)
    {
        _sectionsRepository = sectionsRepository;
    }

    public async Task<GetAllSectionResponse> GetAllAsync(CancellationToken ct = default)
    {
        var sections = await _sectionsRepository.GetAllAsync();
        var mapper = new SectionsMapper();
        var sectionsDto = sections.Select(s => mapper.MapToDto(s));
        
        return Ok<GetAllSectionResponse>(response => { response.Sections = sectionsDto; });
    }
}