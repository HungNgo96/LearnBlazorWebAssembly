using ApplicationClient.Interfaces;
using ApplicationClient.Responses;
using Microsoft.AspNetCore.Components;
using Newtonsoft.Json;
using Shared.Model.Paging;
using Shared.Requests;

namespace BlazorWebApp.Pages;

public partial class Products
{
    public List<ProductResponse> ProductList { get; set; } = new List<ProductResponse>();
    public MetaData MetaData { get; set; } = new MetaData();

    [Inject]
    public IProductService ProductService { get; set; }

    private ProductRequest _productRequest = new ProductRequest();
    private CancellationTokenSource _cts = new();

    protected override async Task OnInitializedAsync()
    {
        await GetProductsAsync();
    }

    private async Task SelectedPage(int page)
    {
        Console.WriteLine("SelectedPage:::" + page);
        _productRequest.PageNumber = page;
        await GetProductsAsync();
    }

    private async Task GetProductsAsync()
    {
        Console.WriteLine("request:" + JsonConvert.SerializeObject(_productRequest));
        var pagingResponse = await ProductService.GetProductsAsync(_productRequest, _cts.Token);
        if (pagingResponse.Succeeded)
        {
            ProductList = pagingResponse.Data.Items;
            MetaData = pagingResponse.Data.MetaData;
        }
    }

    private async Task SearchChanged(string searchTerm)
    {
        Console.WriteLine(searchTerm);
        _productRequest.PageNumber = 1;
        _productRequest.SearchTerm = searchTerm;
        await GetProductsAsync();
    }

    private async Task SortChanged(string orderBy)
    {
        Console.WriteLine(orderBy);
        _productRequest.OrderBy = orderBy;
        await GetProductsAsync();
    }
}