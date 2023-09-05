using AlwaysForum.Api.Models.Api.Posts;
using AlwaysForum.Api.Models.Api.Sections;

namespace AlwaysForum.Api.Services.Posts;

public interface IPostsService
{
    Task<GetPostsForSectionResponse> GetForSectionAsync(GetPostsForSectionRequest request, CancellationToken ct = default);

    Task<GetPostResponse> GetAsync(GetPostRequest request, CancellationToken ct = default);

    Task<CreatePostResponse> AddAsync(CreatePostRequest request, CancellationToken ct = default);

    Task<UpdatePostResponse> UpdateAsync(UpdatePostRequest request, CancellationToken ct = default);

    Task<DeletePostResponse> DeleteAsync(DeletePostRequest request, CancellationToken ct = default);
}