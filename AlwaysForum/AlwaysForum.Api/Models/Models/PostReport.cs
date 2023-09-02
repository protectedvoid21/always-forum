namespace AlwaysForum.Api.Models.Models;

public class PostReport
{
    public int Id { get; set; }

    public int PostId { get; set; }
    public Post Post { get; set; } = null!;

    public string? Description { get; set; }
    public DateTime CreateDate { get; set; }

    public string AuthorId { get; set; } = null!;
    public ForumUser Author { get; set; } = null!;

    public int ReportTypeId { get; set; }
    public ReportType ReportType { get; set; } = null!;
}
