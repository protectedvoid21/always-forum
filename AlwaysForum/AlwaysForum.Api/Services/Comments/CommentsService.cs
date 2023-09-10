using AlwaysForum.Api.Models.Api.Comments;
using AlwaysForum.Api.Models.Api.Posts;
using AlwaysForum.Api.Models.Models;
using AlwaysForum.Api.Repositories.Comments;
using AlwaysForum.Api.Services.Token;
using AlwaysForum.Api.Utils;
using AlwaysForum.Api.Utils.Authentication;
using AlwaysForum.Api.Utils.Mappings;

namespace AlwaysForum.Api.Services.Comments;

public class CommentsService : ServiceBase, ICommentsService
{
    private readonly ICommentsRepository _commentsRepository;
    private readonly CurrentUser _currentUser;
    private readonly ITokenService _tokenService;
    
    public CommentsService(ICommentsRepository commentsRepository, ITokenService tokenService, CurrentUser currentUser)
    {
        _commentsRepository = commentsRepository;
        _tokenService = tokenService;
        _currentUser = currentUser;
    }
    
    public async Task<GetCommentsForPostResponse> GetCommentsForPostAsync(GetCommentsForPostRequest request, CancellationToken ct = default)
    {
        var comments = await _commentsRepository.GetForPostAsync(request.PostId, ct);
        var mapper = new CommentsMapper();
        var commentsDto = comments.Select(s => mapper.MapToDto(s));

        return Ok<GetCommentsForPostResponse>(response => { response.Comments = commentsDto; });
    }
    
    public async Task<ResponseBase> AddCommentAsync(AddCommentRequest request, CancellationToken ct = default)
    {
        var comment = new Comment
        {
            PostId = request.PostId,
            Description = request.Description,
            CreatedTime = DateTime.UtcNow,
            AuthorId = _currentUser.Id
        };
        
        await _commentsRepository.AddAsync(comment, ct);
        var mapper = new CommentsMapper();
        var commentDto = mapper.MapToDto(comment);

        return Ok<ResponseBase>();
    }
}
    
    
