using AlwaysForum.Api.Models.Api.Posts;
using AlwaysForum.Api.Services.Posts;
using Microsoft.AspNetCore.Mvc;

namespace AlwaysForum.Api.Controllers;

public class PostsController : ApiController
{
    private readonly IPostsService _postsService;
    
    public PostsController(IPostsService postsService)
    {
        _postsService = postsService;
    }
    
    [HttpGet("{sectionId:int}")]
    public async Task<GetAllPostsResponse> GetForSection(int sectionId)
    {
        return await _postsService.GetForSectionAsync(sectionId);
    }
}