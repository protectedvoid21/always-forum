using AlwaysForum.Api.Models.Api.Sections;
using AlwaysForum.Api.Repositories.Sections;

namespace AlwaysForum.Api.Services.Sections;

public class SectionsService : ISectionsService
{
    private readonly ISectionsRepository _sectionsRepository;
    
    public SectionsService(ISectionsRepository sectionsRepository)
    {
        _sectionsRepository = sectionsRepository;
    }
    
    public async Task<GetAllSectionResponse> GetAllAsync()
    {
        var sections = await _sectionsRepository.GetAllAsync();

        var response = new GetAllSectionResponse
        {
            StatusCode = 200,
            Sections = sections
        };
        return response;
    }
}