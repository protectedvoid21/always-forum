namespace AlwaysForum.Api.Models.Models;

public class Section
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public List<Post> Posts { get; set; } = new();
}