using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace E_Tracker.Application;

public static class ServiceRegistration
{
    public static void AddInfrastructureServices(this IServiceCollection services)
    {
        services.AddMediatR(typeof(ServiceRegistration));
        services.AddHttpClient();
    }
}