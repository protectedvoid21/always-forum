using AlwaysForum.Api.Models.Dtos.Sections;
using AlwaysForum.Api.Models.Models;
using AlwaysForum.Api.Utils;

namespace AlwaysForum.Api.Models.Api.Sections;

public class GetAllSectionResponse : ResponseBase 
{
    public IEnumerable<SectionDto> Sections { get; set; } = null!;
}