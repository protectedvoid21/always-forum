using AlwaysForum.Api.Models.Dtos.Posts;
using AlwaysForum.Api.Utils;

namespace AlwaysForum.Api.Models.Api.Sections;

public class GetPostsForSectionResponse : ResponseBase
{
    public int SectionId { get; set; }
    public IEnumerable<PostDto> Posts { get; set; } = new List<PostDto>();
}