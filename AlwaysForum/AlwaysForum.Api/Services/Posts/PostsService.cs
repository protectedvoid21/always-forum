using AlwaysForum.Api.Models.Api.Posts;
using AlwaysForum.Api.Models.Api.Sections;
using AlwaysForum.Api.Models.Models;
using AlwaysForum.Api.Repositories.Comments;
using AlwaysForum.Api.Repositories.Posts;
using AlwaysForum.Api.Services.Comments;
using AlwaysForum.Api.Utils;
using AlwaysForum.Api.Utils.Mappings;

namespace AlwaysForum.Api.Services.Posts;

public class PostsService : ServiceBase, IPostsService
{
    private readonly IPostsRepository _postsRepository;
    private readonly ICommentsRepository _commentsRepository;

    public PostsService(IPostsRepository postsRepository, ICommentsRepository commentsRepository)
    {
        _postsRepository = postsRepository;
        _commentsRepository = commentsRepository;
    }

    public async Task<GetPostsForSectionResponse> GetForSectionAsync(GetPostsForSectionRequest request, CancellationToken ct = default)
    {
        var posts = await _postsRepository.GetForSectionAsync(request.SectionId, ct);
        var mapper = new PostsMapper();
        var postsDto = posts.Select(s => mapper.MapToDto(s));

        return Ok<GetPostsForSectionResponse>(response => { response.Posts = postsDto; });
    }

    public async Task<GetPostResponse> GetAsync(GetPostRequest request, CancellationToken ct = default)
    {
        var post = await _postsRepository.GetAsync(request.Id, ct);
        if (post is null)
        {
            return NotFound<GetPostResponse>();
        }
        var mapper = new PostsMapper();
        var postDto = mapper.MapToDto(post);

        return Ok<GetPostResponse>(response => { response.Post = postDto; });
    }

    public async Task<CreatePostResponse> AddAsync(CreatePostRequest request, CancellationToken ct = default)
    {
        var post = new Post
        {
            Title = request.Title,
            Description = request.Description,
            SectionId = request.SectionId,
            CreatedDate = DateTime.Now,
        };

        await _postsRepository.AddAsync(post, ct);

        return Ok<CreatePostResponse>();
    }

    public async Task<UpdatePostResponse> UpdateAsync(UpdatePostRequest request, CancellationToken ct = default)
    {
        var post = await _postsRepository.GetAsync(request.PostId, ct);
        if (post is null)
        {
            return NotFound<UpdatePostResponse>();
        }
        
        post.Title = request.Title;
        post.Description = request.Description;

        await _postsRepository.UpdateAsync(post, ct);

        return Ok<UpdatePostResponse>();
    }

    public async Task<DeletePostResponse> DeleteAsync(DeletePostRequest request, CancellationToken ct = default)
    {
        var post = await _postsRepository.GetAsync(request.PostId, ct);
        if (post is null)
        {
            return NotFound<DeletePostResponse>();
        }
        await _postsRepository.DeleteAsync(post, ct);

        return Ok<DeletePostResponse>();
    }
}