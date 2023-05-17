﻿using ApplicationClient.Responses;
using ApplicationClient.Responses.Paging;
using Shared.Requests;
using Shared.Wrapper;

namespace ApplicationClient.Interfaces
{
    public interface IProductService
    {
        Task<IResult<IEnumerable<ProductResponse>>> GetProductsAsync();
        Task<IResult<PagingResponse<ProductResponse>>> GetProductsAsync(ProductRequest request, CancellationToken cancellationToken);
    }
}
