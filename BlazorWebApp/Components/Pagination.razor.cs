using ApplicationClient.Model;
using Microsoft.AspNetCore.Components;
using Shared.Model.Paging;

namespace BlazorWebApp.Components
{
    public partial class Pagination
    {
        [Parameter]
        public MetaData MetaData { get; set; }
        [Parameter]
        public int Spread { get; set; }
        [Parameter]
        public EventCallback<int> SelectedPage { get; set; }

        private List<PagingLinkModel> _links;

        protected override void OnParametersSet()
        {
            CreatePaginationLinks();
        }

        private void CreatePaginationLinks()
        {
            _links = new List<PagingLinkModel>();

            _links.Add(new PagingLinkModel(MetaData.CurrentPage - 1, MetaData.HasPrevious, "Previous"));

            for (int i = 1; i <= MetaData.TotalPages; i++)
            {
                if (i >= MetaData.CurrentPage - Spread && i <= MetaData.CurrentPage + Spread)
                {
                    _links.Add(new PagingLinkModel(i, true, i.ToString()) { Active = MetaData.CurrentPage == i });
                }
            }

            _links.Add(new PagingLinkModel(MetaData.CurrentPage + 1, MetaData.HasNext, "Next"));
        }

        private async Task OnSelectedPage(PagingLinkModel link)
        {
            if (link.Page == MetaData.CurrentPage || !link.Enabled)
                return;

            MetaData.CurrentPage = link.Page;
            await SelectedPage.InvokeAsync(link.Page);
        }
    }
}
