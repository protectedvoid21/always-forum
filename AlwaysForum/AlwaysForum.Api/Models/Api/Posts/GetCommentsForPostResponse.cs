using AlwaysForum.Api.Models.Dtos.Comments;
using AlwaysForum.Api.Models.Models;
using AlwaysForum.Api.Utils;
using Azure;

namespace AlwaysForum.Api.Models.Api.Posts;

public class GetCommentsForPostResponse : ResponseBase
{
    public IEnumerable<CommentDto> Comments { get; set; } = new List<CommentDto>();
}