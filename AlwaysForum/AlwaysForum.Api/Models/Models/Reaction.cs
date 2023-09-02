namespace AlwaysForum.Api.Models.Models;

public class Reaction
{
    public int Id { get; set; }

    public ReactionType ReactionType { get; set; }
    
    public int PostId { get; set; }
    public Post Post { get; set; } = null!;

    public string UserId { get; set; } = null!;
    public ForumUser User { get; set; } = null!;
}