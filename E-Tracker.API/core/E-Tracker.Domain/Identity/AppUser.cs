using Microsoft.AspNetCore.Identity;

namespace E_Tracker.Domain.Identity;

public class AppUser: IdentityUser<string>
{
    public string SureName { get; set; }
    public string? RefreshToken  { get; set; }
    public DateTime? RefreshTokenEndDate { get; set; }
}