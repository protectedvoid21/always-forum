namespace AlwaysForum.Api.Models.Models;

public class CommentVote
{
    public int Id { get; set; }

    public Comment Comment { get; set; } = null!;
    public int CommentId { get; set; }

    public string AuthorId { get; set; } = null!;
    public ForumUser Author { get; set; } = null!;

    public bool IsUpVote { get; set; }
}