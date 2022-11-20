using E_Tracker.Application.Abstractions.Hubs;
using E_Tracker.SignalR.Hubs;
using Microsoft.AspNetCore.SignalR;

namespace E_Tracker.SignalR.HubService
{
    public class ProductHubService: IHubProductService
    {
        private readonly IHubContext<ProductHub> _hubContext;

        public ProductHubService(IHubContext<ProductHub> hubContext)
        {
            _hubContext = hubContext;
        }

        public async Task AddProductMessageAsync(string message)
        {
            await _hubContext.Clients.All.SendAsync(ReceiveFunctionNames.AddedProductMessage, message);
        } 
    }
}
