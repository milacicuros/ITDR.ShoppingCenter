﻿namespace ITDR.ShoppingCenter.Services.CouponAPI.DTOs;

public class CouponDTO
{
    public int CouponId { get; set; }

    public string CouponCode { get; set; }
    
    public double DiscountAmount { get; set; }
}