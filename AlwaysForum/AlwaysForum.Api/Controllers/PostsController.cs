using AlwaysForum.Api.Models.Api.Posts;
using AlwaysForum.Api.Models.Api.Sections;
using AlwaysForum.Api.Services.Comments;
using AlwaysForum.Api.Services.Posts;
using Microsoft.AspNetCore.Mvc;

namespace AlwaysForum.Api.Controllers;

public class PostsController : ApiController
{
    private readonly IPostsService _postsService;
    private readonly ICommentsService _commentsService;
    
    public PostsController(IPostsService postsService, ICommentsService commentsService)
    {
        _postsService = postsService;
        _commentsService = commentsService;
    }
    
    [HttpGet("{request.Id:int}")]
    public async Task<GetPostResponse> Get([FromRoute] GetPostRequest request)
    {
        return await _postsService.GetAsync(request);
    }
    
    [HttpGet("{request.PostId:int}/comments")]
    public async Task<GetCommentsForPostResponse> GetCommentsForPost([FromRoute] GetCommentsForPostRequest request)
    {
        return await _commentsService.GetCommentsForPostAsync(request);
    }
    
    [HttpPost]
    public async Task<CreatePostResponse> Add([FromBody] CreatePostRequest request)
    {
        return await _postsService.AddAsync(request);
    }
    
    [HttpPut("{request.PostId:int}")]
    public async Task<UpdatePostResponse> Update(UpdatePostRequest request)
    {
        return await _postsService.UpdateAsync(request);
    }
    
    [HttpDelete("{request.PostId:int}")]
    public async Task<DeletePostResponse> Delete(DeletePostRequest request)
    {
        return await _postsService.DeleteAsync(request);
    }
}