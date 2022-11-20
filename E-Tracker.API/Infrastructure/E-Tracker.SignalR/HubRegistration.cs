using E_Tracker.SignalR.Hubs;
using Microsoft.AspNetCore.Builder;

namespace E_Tracker.SignalR
{
    public static class HubRegistration
    {
        public static void MapHubs(this WebApplication application)
        {
            application.MapHub<ProductHub>("/products-hub");
        }
    }
}
