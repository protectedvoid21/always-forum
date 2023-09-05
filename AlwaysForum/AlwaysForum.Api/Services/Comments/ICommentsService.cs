using AlwaysForum.Api.Models.Api.Posts;

namespace AlwaysForum.Api.Services.Comments;

public interface ICommentsService
{
    Task<GetCommentsForPostResponse> GetCommentsForPostAsync(GetCommentsForPostRequest request, CancellationToken ct = default);
}