using AlwaysForum.Api.Models.Dtos.Posts;
using AlwaysForum.Api.Models.Models;
using Riok.Mapperly.Abstractions;

namespace AlwaysForum.Api.Utils.Mappings;

[Mapper]
public partial class PostsMapper
{
    public partial PostDto MapToDto(Post post);
}