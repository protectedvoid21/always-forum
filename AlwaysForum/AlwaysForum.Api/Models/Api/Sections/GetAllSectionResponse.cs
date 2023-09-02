using AlwaysForum.Api.Models.Models;
using AlwaysForum.Api.Utils;

namespace AlwaysForum.Api.Models.Api.Sections;

public class GetAllSectionResponse : ResponseBase 
{
    public IEnumerable<Section> Sections { get; set; } = null!;
}