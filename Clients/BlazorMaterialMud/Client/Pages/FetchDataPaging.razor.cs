using ApplicationClient.Interfaces;
using ApplicationClient.Responses;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using Shared.Model.Paging;
using Shared.Requests.Products;

namespace BlazorMaterialMud.Client.Pages
{
    public sealed partial class FetchDataPaging : IDisposable
    {
        private CancellationTokenSource cts = new CancellationTokenSource();
        private ProductRequest _productRequest = new ProductRequest();
        public MetaData MetaData { get; set; } = new MetaData();
        private MudTable<ProductResponse> _table;
        private readonly int[] _pageSizeOption = { 4, 6, 10 };
        [Inject]
        private IProductService ProductService { get; set; }

        public List<ProductResponse> Products { get; set; } = new List<ProductResponse>();

        protected override async Task OnInitializedAsync()
        {
            //await GetProductsAsync();
        }

        public void Dispose()
        {
            cts.Cancel();
            cts.Dispose();
        }

        private async Task GetProductsAsync()
        {
            var pagingResponse = await ProductService.GetProductsAsync(_productRequest, cts.Token);

            if (pagingResponse.Succeeded)
            {
                Products = pagingResponse.Data.Items;
                MetaData = pagingResponse.Data.MetaData;
            }
        }

        private async Task<TableData<ProductResponse>> GetServerData(TableState state)
        {
            _productRequest.PageSize = state.PageSize;
            _productRequest.PageNumber = state.Page + 1;
            _productRequest.OrderBy = state.SortDirection == SortDirection.Descending ?
           state.SortLabel + " desc" :
           state.SortLabel;
            var response = await ProductService.GetProductsAsync(_productRequest, cts.Token);
            if (response.Succeeded)
            {
                return new TableData<ProductResponse>
                {
                    Items = response.Data.Items,
                    TotalItems = response.Data.MetaData.TotalCount
                };
            }

            return new TableData<ProductResponse>
            {
                Items = new List<ProductResponse>(),
                TotalItems = 0
            };

        }

        private async Task OnSearch(string searchTerm)
        {
            _productRequest.SearchTerm = searchTerm;
            _ = _table.ReloadServerData();
        }
    }
}
