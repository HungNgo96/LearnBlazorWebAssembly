using ApplicationClient.Interfaces;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using MudBlazor;
using System.Net.Http.Headers;

namespace BlazorMaterialMud.Client.Components
{
    public partial class ImageUpload
    {
        public string ImgUrl { get; set; }
        [Inject]
        public ISnackbar Snackbar { get; set; }
        [Parameter]
        public EventCallback<string> OnChange { get; set; }

        [Inject]
        private IProductService _productService { get; set; }

        private async Task UploadImage(InputFileChangeEventArgs e)
        {
            var imageFiles = e.GetMultipleFiles();
            foreach (var imageFile in imageFiles)
            {
                if (imageFile != null)
                {
                    var resizedFile = await imageFile.RequestImageFileAsync("image/png", 300, 500);

                    using (var ms = resizedFile.OpenReadStream(resizedFile.Size))
                    {
                        var content = new MultipartFormDataContent();
                        content.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data");
                        content.Add(new StreamContent(ms, Convert.ToInt32(resizedFile.Size)), "image", imageFile.Name);
                        var result = await _productService.UploadProductImage(content);
                        ImgUrl = result.Data;
                        await OnChange.InvokeAsync(ImgUrl);
                    }
                }
            }
            Snackbar.Add("Image uploaded successfully.", Severity.Info);
        }
    }
}