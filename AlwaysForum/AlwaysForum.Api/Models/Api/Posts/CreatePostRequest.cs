namespace AlwaysForum.Api.Models.Api.Posts;

public class CreatePostRequest
{
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
    public int SectionId { get; set; }
}