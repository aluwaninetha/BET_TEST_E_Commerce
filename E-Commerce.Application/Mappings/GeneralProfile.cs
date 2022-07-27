using AutoMapper;
using E_Commerce.Application.Features.Products.Queries.GetAllProducts;
using E_Commerce.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace E_Commerce.Application.Mappings
{
    public class GeneralProfile : Profile
    {
        public GeneralProfile()
        {
            CreateMap<Product, GetAllProductsViewModel>().ReverseMap();
            CreateMap<GetAllProductsQuery, GetAllProductsParameter>();
        }
    }
}