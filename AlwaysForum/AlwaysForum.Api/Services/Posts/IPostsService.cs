using AlwaysForum.Api.Models.Api.Posts;
using AlwaysForum.Api.Models.Api.Sections;
using AlwaysForum.Api.Utils;

namespace AlwaysForum.Api.Services.Posts;

public interface IPostsService
{
    Task<GetPostsForSectionResponse> GetForSectionAsync(GetPostsForSectionRequest request, CancellationToken ct = default);

    Task<GetPostResponse> GetAsync(GetPostRequest request, CancellationToken ct = default);

    Task<ResponseBase> AddAsync(CreatePostRequest request, CancellationToken ct = default);

    Task<ResponseBase> UpdateAsync(UpdatePostRequest request, CancellationToken ct = default);

    Task<ResponseBase> DeleteAsync(DeletePostRequest request, CancellationToken ct = default);
}