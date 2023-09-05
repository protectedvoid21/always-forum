using AlwaysForum.Api.Models.Dtos.Comments;
using AlwaysForum.Api.Models.Models;
using Riok.Mapperly.Abstractions;

namespace AlwaysForum.Api.Utils.Mappings;

[Mapper]
public partial class CommentsMapper
{
    public partial CommentDto MapToDto(Comment comment);
}