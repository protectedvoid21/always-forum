using System.Security.Claims;
using AlwaysForum.Api.Models.Models;

namespace AlwaysForum.Api.Utils.Authentication;

public class CurrentUser
{
    public ForumUser User { get; set; } = null!;
    public ClaimsPrincipal Principal { get; set; } = default!;

    public string Id => Principal.FindFirstValue(ClaimTypes.NameIdentifier)!;
    public bool IsAdmin => Principal.IsInRole("Admin");
}