using AlwaysForum.Api.Models.Api.Posts;
using AlwaysForum.Api.Models.Api.Sections;
using AlwaysForum.Api.Services.Posts;
using AlwaysForum.Api.Services.Sections;
using Microsoft.AspNetCore.Mvc;

namespace AlwaysForum.Api.Controllers;

public class SectionsController : ApiController
{
    private readonly ISectionsService _sectionsService;
    private readonly IPostsService _postsService;
    
    public SectionsController(ISectionsService sectionsService, IPostsService postsService)
    {
        _sectionsService = sectionsService;
        _postsService = postsService;
    }
    
    [HttpGet]
    public async Task<GetAllSectionResponse> GetAll()
    {
        return await _sectionsService.GetAllAsync();
    }
    
    [HttpGet("{request.SectionId:int}/posts")]
    public async Task<GetPostsForSectionResponse> GetPostsForSection([FromRoute] GetPostsForSectionRequest request)
    {
        return await _postsService.GetForSectionAsync(request); 
    }
}