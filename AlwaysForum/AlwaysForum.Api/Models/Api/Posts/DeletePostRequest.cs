using AlwaysForum.Api.Utils;

namespace AlwaysForum.Api.Models.Api.Posts;

public class DeletePostRequest : ResponseBase
{
    public int PostId { get; set; }
}