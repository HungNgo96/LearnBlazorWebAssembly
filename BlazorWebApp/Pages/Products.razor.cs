using ApplicationClient.Interfaces;
using ApplicationClient.Responses;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Shared.Model.Paging;
using Shared.Requests;

namespace BlazorWebApp.Pages;

public sealed partial class Products : IDisposable
{
    private CancellationTokenSource cts = new CancellationTokenSource();
    public List<ProductResponse> ProductList { get; set; } = new List<ProductResponse>();
    public MetaData MetaData { get; set; } = new MetaData();

    [Inject]
    public IProductService ProductService { get; set; }
    [Inject]
    public ILogger<Products> Logger { get; set; }

    private ProductRequest _productRequest = new ProductRequest();
    private CancellationTokenSource _cts = new();
    public void Dispose()
    {
        cts.Cancel();
        cts.Dispose();
    }
    protected override async Task OnInitializedAsync()
    {
        await GetProductsAsync();
    }

    private async Task SelectedPage(int page)
    {

        _productRequest.PageNumber = page;
        await GetProductsAsync();
    }

    private async Task GetProductsAsync()
    {

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

    private async Task DeleteProduct(Guid id)
    {
        Console.WriteLine("DeleteProduct request");
        var result = await ProductService.DeleteProductAsync(id, cts.Token);
        if(result.Succeeded)
        {
            _productRequest.PageNumber = 1;
            await GetProductsAsync();
        }

        Console.WriteLine("DeleteProduct response");
    }
}