using AlwaysForum.Api.Models.Dtos.Posts;
using AlwaysForum.Api.Utils;

namespace AlwaysForum.Api.Models.Api.Posts;

public class GetPostResponse : ResponseBase
{
    public PostDto Post { get; set; } = null!;
}