namespace AlwaysForum.Api.Models.Dtos.Posts;

public record PostDto(int Id, string Title, string Description, string AuthorId, DateTimeOffset CreatedDate);