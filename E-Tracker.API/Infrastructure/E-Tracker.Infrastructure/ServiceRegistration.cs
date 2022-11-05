using E_Tracker.Application.Abstractions;
using E_Tracker.Application.Abstractions.Storage;
using E_Tracker.Application.Abstractions.Token;
using E_Tracker.Infrastructure.Enums;
using E_Tracker.Infrastructure.Services;
using E_Tracker.Infrastructure.Services.Storage;
using E_Tracker.Infrastructure.Services.Storage.Azure;
using E_Tracker.Infrastructure.Services.Storage.Local;
using E_Tracker.Infrastructure.Services.Token;
using Microsoft.Extensions.DependencyInjection;

namespace E_Tracker.Infrastructure
{
    public static class ServiceRegistration
    {
        public static void AddInfrastructureService(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<IStorageService, StorageService>();
            serviceCollection.AddScoped<ITokenHandler, TokenHandler>();
        }

        public static void AddStorage<T>(this IServiceCollection serviceCollection) where T : Storage, IStorage
        {
            serviceCollection.AddScoped<IStorage, T>();
        }
        
        public static void AddStorage(this IServiceCollection serviceCollection, LocalStorageType storageType) 
        {
            switch (storageType)
            {
                case LocalStorageType.AWS:
                    break;
                case LocalStorageType.Azure:
                    serviceCollection.AddScoped<IStorage, AzureStorage>();
                    break;
                case LocalStorageType.Local:
                    serviceCollection.AddScoped<IStorage, LocalStorage>();
                    break;
                default:
                    serviceCollection.AddScoped<IStorage, LocalStorage>();
                    break;
                
            }
        }
    }
}
