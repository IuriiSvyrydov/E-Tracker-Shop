

namespace E_Tracker.Application.Abstractions.Hubs
{
    public interface IHubProductService
    {
        Task AddProductMessageAsync(string message);
    }
}
