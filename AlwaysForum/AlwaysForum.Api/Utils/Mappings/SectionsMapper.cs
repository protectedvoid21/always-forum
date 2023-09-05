using AlwaysForum.Api.Models.Dtos.Sections;
using AlwaysForum.Api.Models.Models;
using Riok.Mapperly.Abstractions;

namespace AlwaysForum.Api.Utils.Mappings;

[Mapper]
public partial class SectionsMapper
{
    public partial SectionDto MapToDto(Section section);
}