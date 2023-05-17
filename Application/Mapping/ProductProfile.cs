﻿using AutoMapper;
using Domain.Entity;
using Shared.Requests;
using Shared.Responses.Products;
using Shared.Wrapper;

namespace FTI.PartnerMiddle.Application.Mapping.Deploys
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            _ = CreateMap<ProductResponse, Product>().ReverseMap();
            _ = CreateMap<CreateProductRequest, Product>().ReverseMap();
        }
    }
}