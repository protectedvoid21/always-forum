namespace AlwaysForum.Api.Models.Api.Posts;

public class UpdatePostRequest
{
    public int PostId { get; set; }
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
}