using AlwaysForum.Api.Models.Dtos.Posts;
using AlwaysForum.Api.Utils;

namespace AlwaysForum.Api.Models.Api.Posts;

public class GetAllPostsResponse : ResponseBase
{
    public IEnumerable<PostDto> Posts { get; set; } = new List<PostDto>();
}