using E_Tracker.Application.Abstractions.Hubs;
using E_Tracker.SignalR.HubService;
using Microsoft.Extensions.DependencyInjection;

namespace E_Tracker.SignalR
{
    public static class ServiceRegistration
    {
        public static void AddSignalRService(this IServiceCollection services)
        {
            services.AddTransient<IHubProductService, ProductHubService>();
            services.AddSignalR();
        }
    }
}
