namespace AlwaysForum.Api.Models.Models;

public class CommentReport
{
    public int Id { get; set; }

    public int CommentId { get; set; }
    public Comment Comment { get; set; } = null!;

    public string? Description { get; set; }
    public DateTime CreateDate { get; set; }
    
    public string AuthorId { get; set; } = null!;
    public ForumUser Author { get; set; } = null!;

    public int ReportTypeId { get; set; }
    public ReportType ReportType { get; set; } = null!;
}