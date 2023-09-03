using AlwaysForum.Api.Models.Api.Posts;

namespace AlwaysForum.Api.Services.Posts;

public interface IPostsService
{
    Task<GetAllPostsResponse> GetForSectionAsync(int sectionId);
}