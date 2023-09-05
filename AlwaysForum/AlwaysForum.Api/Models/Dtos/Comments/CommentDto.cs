namespace AlwaysForum.Api.Models.Dtos.Comments;

public record CommentDto(int Id, string Description, string? AuthorId, DateTime CreatedTime);