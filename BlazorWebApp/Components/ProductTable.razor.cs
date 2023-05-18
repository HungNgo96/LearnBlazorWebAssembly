using Microsoft.AspNetCore.Components;
using ApplicationClient.Responses;
using Microsoft.JSInterop;

namespace BlazorWebApp.Components;

public partial class ProductTable
{
    [Parameter]
    public EventCallback<Guid> OnDeleted { get; set; }
    [Parameter]
    public List<ProductResponse> Products { get; set; }
    [Inject]
    public NavigationManager NavigationManager { get; set; }
    [Inject]
    public IJSRuntime Js { get; set; }

    [Parameter]
    public bool IsErrorTable { get; set; }

    private void RedirectToUpdate(Guid id)
    {
        var url = Path.Combine("/ll/updateProduct/", id.ToString());
        NavigationManager.NavigateTo(url);
    }

    private async Task Delete(Guid id)
    {
        var product = Products.FirstOrDefault(p => p.Id.Equals(id));

        var confirmed = await Js.InvokeAsync<bool>("confirm", $"Are you sure you want to delete {product?.Id} product?");
        if (confirmed)
        {
            await OnDeleted.InvokeAsync(id);
        }
    }
}