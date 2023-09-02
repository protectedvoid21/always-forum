namespace AlwaysForum.Api.Models.Models;

public class Message
{
    public int Id { get; set; }

    public string Text { get; set; } = null!;
    public bool IsDeleted { get; set; }
    public DateTime SendDate { get; set; }

    public string AuthorId { get; set; } = null!;
    public ForumUser Author { get; set; } = null!;
}