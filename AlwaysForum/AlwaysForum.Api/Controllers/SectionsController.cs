using AlwaysForum.Api.Models.Api.Sections;
using AlwaysForum.Api.Services.Sections;
using Microsoft.AspNetCore.Mvc;

namespace AlwaysForum.Api.Controllers;

public class SectionsController : ApiController
{
    private readonly ISectionsService _sectionsService;
    
    public SectionsController(ISectionsService sectionsService)
    {
        _sectionsService = sectionsService;
    }
    
    [HttpGet]
    public async Task<GetAllSectionResponse> GetAll()
    {
        return await _sectionsService.GetAllAsync();
    }
}