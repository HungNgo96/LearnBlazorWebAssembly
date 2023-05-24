using ApplicationClient.Interfaces;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;
using Newtonsoft.Json;
using Shared.Model.Charts;
using System;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace BlazorMaterialMud.Client.Pages
{
    public partial class SignalRCharts : IDisposable
    {
        private CancellationTokenSource cts = new CancellationTokenSource();
        private HubConnection _hubConnection;
        public List<ChartModel> Data = new List<ChartModel>();
        public List<ChartModel> ExchangedData = new List<ChartModel>();

        [Inject]
        private IProductService ProductService { get; set; }

        protected async override Task OnInitializedAsync()
        {
            await StartHubConnection();

            await ProductService.CallChartEndpoint(cts.Token);

            AddTransferChartDataListener();
            AddExchangeDataListener();
        }

        private async Task StartHubConnection()
        {
            _hubConnection = new HubConnectionBuilder()
                .WithUrl("http://localhost:5211/chart")//connection port API hub
                .Build();

            await _hubConnection.StartAsync();
            if (_hubConnection.State == HubConnectionState.Connected)
                Console.WriteLine("connection started");
        }

        public async Task SendToAcceptChartDataMethod()
        {
            Console.WriteLine("1. Send server hub " + DateTime.Now.ToLongTimeString() + "::SendToAcceptChartDataMethod::" + JsonConvert.SerializeObject(Data));
            await _hubConnection.SendAsync("AcceptChartData", Data);
        }

        private void AddTransferChartDataListener()
        {
            _hubConnection.On<List<ChartModel>>("TransferChartData", (data) =>
            {
                Console.WriteLine("2. Send server hub " + DateTime.Now.ToLongTimeString() + "::AddTransferChartDataListener::" + JsonConvert.SerializeObject(Data));
                foreach (var item in data)
                {
                    Console.WriteLine($"Label: {item.Label}, Value: {item.Value}");
                }
                Data = data;
                StateHasChanged();
            });
        }


        private void AddExchangeDataListener()
        {
            _hubConnection.On<List<ChartModel>>("ExchangeChartData", (data) =>
            {
                Console.WriteLine("3. recive server " + DateTime.Now.ToLongTimeString() + "::AddExchangeDataListener::" + JsonConvert.SerializeObject(data));
                ExchangedData = data;
                StateHasChanged();
            });
        }

        public void Dispose()
        {
            cts.Cancel();
            cts.Dispose();
            _hubConnection.DisposeAsync();
        }
    }
}
