namespace AlwaysForum.Api.Utils.Settings;

public class JwtSettings
{
    public string Key { get; set; } = null!;
    public string Issuer { get; set; } = null!;
    public int ExpirationDays { get; set; }
}