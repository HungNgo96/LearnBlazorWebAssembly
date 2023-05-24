using ApplicationClient.Interfaces;
using ApplicationClient.Responses;
using ApplicationClient.Services;
using Microsoft.AspNetCore.Components;
using Shared.Model.Paging;
using Shared.Requests.Products;

namespace BlazorWebApp.Pages;

public sealed partial class Products : IDisposable
{
    private CancellationTokenSource cts = new CancellationTokenSource();
    public List<ProductResponse> ProductList { get; set; } = new List<ProductResponse>();
    public MetaData MetaData { get; set; } = new MetaData();

    [Inject]
    private IProductService ProductService { get; set; }
    [Inject]
    public HttpInterceptorService Interceptor { get; set; }
    [Inject]
    public ILogger<Products> Logger { get; set; }
    private bool IsError = false;
    private ProductRequest _productRequest = new ProductRequest();
    public void Dispose()
    {
        cts.Cancel();
        cts.Dispose();
        Interceptor.DisposeEvent();
    }
    protected override async Task OnInitializedAsync()
    {
        Interceptor.RegisterEvent();//refresh token
        Logger.LogInformation("1. OnInitializedAsync Products");
        await GetProductsAsync();
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await Task.Delay(1);
        if (firstRender)
        {
            Logger.LogInformation("2. OnAfterRenderAsync firstRender Products");
        }
        else
        {
            Logger.LogInformation("2. OnInitializedAsync secondRender Products");
        }
    }

    private async Task SelectedPage(int page)
    {

        _productRequest.PageNumber = page;
        await GetProductsAsync();
    }

    private async Task GetProductsAsync()
    {
        var pagingResponse = await ProductService.GetProductsAsync(_productRequest, cts.Token);
        if (pagingResponse.Succeeded)
        {
            ProductList = pagingResponse.Data.Items;
            MetaData = pagingResponse.Data.MetaData;
        }
        else
        {
            IsError = true;
            //StateHasChanged();//re-render manual
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