﻿namespace ITDR.ShoppingCenter.Services.CouponAPI.DTOs;

public class ResponseDTO
{
    public bool IsSuccess { get; set; } = true;
    public object Result { get; set; }
    public string DisplayMessage { get; set; } = "";
    public List<string> ErrorMessage { get; set; }
}