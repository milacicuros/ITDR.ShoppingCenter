﻿using System.Security.Claims;
using IdentityModel;
using ITDR.ShoppingCenter.Services.Identity.DbContexts;
using ITDR.ShoppingCenter.Services.Identity.Models;
using Microsoft.AspNetCore.Identity;

namespace ITDR.ShoppingCenter.Services.Identity.Initializer;

public class DbInitializer : IDbInitializer
{
    private readonly ApplicationDbContext _db;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public DbInitializer(ApplicationDbContext db, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        _db = db;
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public void Initialize()
    {
        if (_roleManager.FindByNameAsync(SD.Admin).Result == null)
        {
            _roleManager.CreateAsync(new IdentityRole(SD.Admin)).GetAwaiter().GetResult();
            _roleManager.CreateAsync(new IdentityRole(SD.Customer)).GetAwaiter().GetResult();
        }
        else
        {
            return;
        }

        var adminUser = new ApplicationUser()
        {
            UserName = "admin1@gmail.com",
            Email = "admin1@gmail.com",
            EmailConfirmed = true,
            PhoneNumber = "111111111111",
            FirstName = "Spriggan",
            LastName = "Admin"
        };

        _userManager.CreateAsync(adminUser, "Admin123!").GetAwaiter().GetResult();
        _userManager.AddToRoleAsync(adminUser, SD.Admin).GetAwaiter().GetResult();

        var temp1 = _userManager.AddClaimsAsync(adminUser, new Claim[]
        {
            new Claim(JwtClaimTypes.Name, adminUser.FirstName + " " + adminUser.LastName),
            new Claim(JwtClaimTypes.GivenName, adminUser.FirstName),
            new Claim(JwtClaimTypes.FamilyName, adminUser.LastName),
            new Claim(JwtClaimTypes.Role, SD.Admin)
        }).Result;

        var customerUser = new ApplicationUser()
        {
            UserName = "customer1@gmail.com",
            Email = "customer1@gmail.com",
            EmailConfirmed = true,
            PhoneNumber = "111111111111",
            FirstName = "Spriggan",
            LastName = "Customer"
        };

        _userManager.CreateAsync(customerUser, "Customer123!").GetAwaiter().GetResult();
        _userManager.AddToRoleAsync(customerUser, SD.Customer).GetAwaiter().GetResult();

        var temp2 = _userManager.AddClaimsAsync(customerUser, new Claim[]
        {
            new Claim(JwtClaimTypes.Name, customerUser.FirstName + " " + customerUser.LastName),
            new Claim(JwtClaimTypes.GivenName, customerUser.FirstName),
            new Claim(JwtClaimTypes.FamilyName, customerUser.LastName),
            new Claim(JwtClaimTypes.Role, SD.Customer)
        }).Result;
}
}