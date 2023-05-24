using Application.ChartDataProvider;
using Application.HubConfig;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Shared.Wrapper;

namespace API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ChartController : ControllerBase
    {
        private readonly IHubContext<ChartHub> _hub;
        private readonly TimerManager _timer;
        public ChartController(IHubContext<ChartHub> hub, TimerManager timer)
        {
            _hub = hub;
            _timer = timer;
        }
        [HttpGet]
        public async Task<IActionResult> GetChartData()
        {
            if (!_timer.IsTimerStarted)
                _timer.PrepareTimer(() => _hub.Clients.All.SendAsync("TransferChartData", DataManager.GetData()));
            //return Ok(new { Message = "Request Completed" });
            return Ok(await Result<string>.SuccessAsync(data: "Request Completed", message: "Request Completed"));
        }
    }
}
