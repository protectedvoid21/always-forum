namespace AlwaysForum.Api.Models.Models;

public class Tag
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public List<Post> Posts { get; set; } = new();
}