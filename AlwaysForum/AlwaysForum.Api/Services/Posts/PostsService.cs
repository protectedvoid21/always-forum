using AlwaysForum.Api.Models.Api.Posts;
using AlwaysForum.Api.Models.Api.Sections;
using AlwaysForum.Api.Models.Models;
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
    
    public async Task<GetPostsForSectionResponse> GetForSectionAsync(GetPostsForSectionRequest request)
    {
        try
        {
            var posts = await _postsRepository.GetForSectionAsync(request.SectionId);
            var mapper = new PostMapper();
            var postsDto = posts.Select(s => mapper.MapToDto(s));
            
            return Ok<GetPostsForSectionResponse>(response => { response.Posts = postsDto; });
        }
        catch (Exception ex)
        {
            return HandleException<GetPostsForSectionResponse>(ex);
        }
    }
    
    public async Task<GetPostResponse> GetAsync(GetPostRequest request)
    {
        try
        {
            var post = await _postsRepository.GetAsync(request.Id);
            var mapper = new PostMapper();
            var postDto = mapper.MapToDto(post);
            
            return Ok<GetPostResponse>(response => { response.Post = postDto; });
        }
        catch (Exception ex)
        {
            return HandleException<GetPostResponse>(ex);
        }
    }
    
    public async Task<CreatePostResponse> AddAsync(CreatePostRequest request)
    {
        try
        {
            var post = new Post
            {
                Title = request.Title,
                Description = request.Description,
                SectionId = request.SectionId,
                CreatedDate = DateTime.Now,
            };
            
            await _postsRepository.AddAsync(post);
            
            return Ok<CreatePostResponse>();
        }
        catch (Exception ex)
        {
            return HandleException<CreatePostResponse>(ex);
        }
    }
    
    public async Task<UpdatePostResponse> UpdateAsync(UpdatePostRequest request)
    {
        try
        {
            var post = await _postsRepository.GetAsync(request.PostId);
            post.Title = request.Title;
            post.Description = request.Description;
            
            await _postsRepository.UpdateAsync(post);
            
            return Ok<UpdatePostResponse>();
        }
        catch (Exception ex)
        {
            return HandleException<UpdatePostResponse>(ex);
        }
    }
    
    public async Task<DeletePostResponse> DeleteAsync(DeletePostRequest request)
    {
        try
        {
            var post = await _postsRepository.GetAsync(request.PostId);
            await _postsRepository.DeleteAsync(post);
            
            return Ok<DeletePostResponse>();
        }
        catch (Exception ex)
        {
            return HandleException<DeletePostResponse>(ex);
        }
    }
}