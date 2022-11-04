using Microsoft.AspNetCore.Identity;

namespace E_Tracker.Domain.Identity;

public class AppUser: IdentityUser<string>
{
    public string SureName { get; set; }
}