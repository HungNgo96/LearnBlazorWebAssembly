using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Shared.Model.Charts;

namespace Application.HubConfig
{
    public class ChartHub : Hub
    {
        private readonly ILogger<ChartHub> _logger;

        public ChartHub(ILogger<ChartHub> logger)
        {
            _logger = logger;
        }

        public async Task AcceptChartData(List<ChartModel> data)
        {
            _logger.LogInformation("ChartHub - AcceptChartData::" + JsonConvert.SerializeObject(data));
            await Clients.All.SendAsync("ExchangeChartData", data);
        }
         
    }
}
