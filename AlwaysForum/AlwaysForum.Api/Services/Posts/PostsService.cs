using AlwaysForum.Api.Models.Api.Posts;
using AlwaysForum.Api.Repositories.Posts;
using AlwaysForum.Api.Utils;
using AlwaysForum.Api.Utils.Mappings;

namespace AlwaysForum.Api.Services.Posts;

public class PostsService : ServiceBase, IPostsService
{
    private readonly IPostsRepository _postsRepository;
    
    public PostsService(IPostsRepository postsRepository)
    {
        _postsRepository = postsRepository;
    }
    
    public async Task<GetAllPostsResponse> GetForSectionAsync(int sectionId)
    {
        try
        {
            var posts = await _postsRepository.GetForSectionAsync(sectionId);
            var mapper = new PostMapper();
            var postsDto = posts.Select(s => mapper.MapToDto(s));
            
            return Ok<GetAllPostsResponse>(response => { response.Posts = postsDto; });
        }
        catch (Exception ex)
        {
            return HandleException<GetAllPostsResponse>(ex);
        }
    }
}