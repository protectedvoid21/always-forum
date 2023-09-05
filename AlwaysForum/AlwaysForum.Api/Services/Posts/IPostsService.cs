using AlwaysForum.Api.Models.Api.Posts;
using AlwaysForum.Api.Models.Api.Sections;

namespace AlwaysForum.Api.Services.Posts;

public interface IPostsService
{
    Task<GetPostsForSectionResponse> GetForSectionAsync(GetPostsForSectionRequest request);

    Task<GetPostResponse> GetAsync(GetPostRequest request);

    Task<CreatePostResponse> AddAsync(CreatePostRequest request);

    Task<UpdatePostResponse> UpdateAsync(UpdatePostRequest request);

    Task<DeletePostResponse> DeleteAsync(DeletePostRequest request);
}