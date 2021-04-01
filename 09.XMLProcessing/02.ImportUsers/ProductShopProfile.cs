﻿using AutoMapper;
using ProductShop.DataTransferObjects;
using ProductShop.Models;

namespace ProductShop
{
    public class ProductShopProfile : Profile
    {
        public ProductShopProfile()
        {
            CreateMap<CategoryProduct, Category>();

            CreateMap<CategoryProduct, Product>();

            CreateMap<ImportUserDto, User>();
        }
    }
}
