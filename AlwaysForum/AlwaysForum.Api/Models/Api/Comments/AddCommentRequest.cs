namespace AlwaysForum.Api.Models.Api.Comments;

public class AddCommentRequest
{
    public int Title { get; set; }
    public string Description { get; set; } = null!;
    public int PostId { get; set; }
}