using AutoMapper;
using Domain.Entity;
using Shared.Responses;

namespace FTI.PartnerMiddle.Application.Mapping.Deploys
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            _ = CreateMap<ProductResponse, Product>().ReverseMap();
        }
    }
}