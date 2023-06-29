using AutoMapper;
using ITDR.ShoppingCenter.Services.CouponAPI.DTOs;
using ITDR.ShoppingCenter.Services.CouponAPI.Models;

namespace ITDR.ShoppingCenter.Services.CouponAPI;

public class MappingConfig
{
    public static MapperConfiguration RegisterMaps()
    {
        var mappingConfig = new MapperConfiguration(config =>
        {
            config.CreateMap<CouponDTO, Coupon>().ReverseMap();
        });

        return mappingConfig;
    }
}