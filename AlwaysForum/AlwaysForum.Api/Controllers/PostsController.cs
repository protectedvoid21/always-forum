using AlwaysForum.Api.Models.Api.Posts;
using AlwaysForum.Api.Models.Api.Sections;
using AlwaysForum.Api.Services.Comments;
using AlwaysForum.Api.Services.Posts;
using AlwaysForum.Api.Utils;
using Microsoft.AspNetCore.Authorization;
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
    
    [HttpPost, Authorize]
    public async Task<ResponseBase> Add([FromBody] CreatePostRequest request)
    {
        return await _postsService.AddAsync(request);
    }
    
    [HttpPut("{request.PostId:int}"), Authorize]
    public async Task<ResponseBase> Update(UpdatePostRequest request)
    {
        return await _postsService.UpdateAsync(request);
    }
    
    [HttpDelete("{request.PostId:int}"), Authorize]
    public async Task<ResponseBase> Delete(DeletePostRequest request)
    {
        return await _postsService.DeleteAsync(request);
    }
}