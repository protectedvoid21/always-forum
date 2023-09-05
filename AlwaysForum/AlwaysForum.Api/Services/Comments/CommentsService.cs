using AlwaysForum.Api.Models.Api.Posts;
using AlwaysForum.Api.Repositories.Comments;
using AlwaysForum.Api.Utils;
using AlwaysForum.Api.Utils.Mappings;

namespace AlwaysForum.Api.Services.Comments;

public class CommentsService : ServiceBase, ICommentsService
{
    private readonly ICommentsRepository _commentsRepository;
    
    public CommentsService(ICommentsRepository commentsRepository)
    {
        _commentsRepository = commentsRepository;
    }
    
    public async Task<GetCommentsForPostResponse> GetCommentsForPostAsync(GetCommentsForPostRequest request, CancellationToken ct = default)
    {
        var comments = await _commentsRepository.GetForPostAsync(request.PostId);
        var mapper = new CommentsMapper();
        var commentsDto = comments.Select(s => mapper.MapToDto(s));

        return Ok<GetCommentsForPostResponse>(response => { response.Comments = commentsDto; });
    }
}