﻿using AutoMapper;
using ITDR.ShoppingCenter.Services.ProductAPI.DTOs;
using ITDR.ShoppingCenter.Services.ProductAPI.Models;

namespace ITDR.ShoppingCenter.Services.ProductAPI;

public class MappingConfig
{
    public static MapperConfiguration RegisterMaps()
    {
        var mappingConfig = new MapperConfiguration(config =>
        {
            config.CreateMap<ProductDTO, Product>();
            config.CreateMap<Product, ProductDTO>();
        });

        return mappingConfig;
    }
}