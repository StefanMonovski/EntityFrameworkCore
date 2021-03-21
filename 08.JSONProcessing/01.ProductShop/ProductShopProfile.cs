﻿using AutoMapper;
using ProductShop.Models;

namespace ProductShop
{
    public class ProductShopProfile : Profile
    {
        public ProductShopProfile()
        {
            CreateMap<CategoryProduct, Category>();

            CreateMap<CategoryProduct, Product>();
        }
    }
}
