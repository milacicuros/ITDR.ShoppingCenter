using AutoMapper;
using ITDR.ShoppingCenter.Services.ShoppingCartAPI.DbContexts;
using ITDR.ShoppingCenter.Services.ShoppingCartAPI.DTOs;
using ITDR.ShoppingCenter.Services.ShoppingCartAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace ITDR.ShoppingCenter.Services.ShoppingCartAPI.Repository;

public class CartRepository : ICartRepository
{
    private readonly ApplicationDbContext _db;
    private IMapper _mapper;

    public CartRepository(ApplicationDbContext db, IMapper mapper)
    {
        _db = db;
        _mapper = mapper;
    }
    
    public async Task<CartDTO> GetCartByUserId(string userId)
    {
        var cart = new Cart()
        {
            CartHeader = await _db.CartHeaders
                .FirstOrDefaultAsync(cartHeader => cartHeader.UserId == userId)
        };
        cart.CartDetails = _db.CartDetails
            .Where(details => details.CartHeaderId == cart.CartHeader.CartHeaderId)
            .Include(details => details.Product);

        return _mapper.Map<CartDTO>(cart);
    }

    public async Task<CartDTO> CreateUpdateCart(CartDTO cartDTO)
    {
        var cart = _mapper.Map<Cart>(cartDTO);
        
        //check if product exists in db, if not then create it
        var productInDb = await _db.Products
            .FirstOrDefaultAsync(product => product.ProductId == cartDTO.CartDetails
                .FirstOrDefault().ProductId);
        
        if (productInDb == null)
        {
            _db.Products.Add(cart.CartDetails.FirstOrDefault().Product);
            await _db.SaveChangesAsync();
        }
        
        //check if header is null
        var cartHeaderFromDb = await _db.CartHeaders
            .AsNoTracking()
            .FirstOrDefaultAsync(cartHeader => cartHeader.UserId == cart.CartHeader.UserId);

        if (cartHeaderFromDb == null)
        {
            //create header and details
            _db.CartHeaders.Add(cart.CartHeader);
            await _db.SaveChangesAsync();
            cart.CartDetails.FirstOrDefault().CartHeaderId = cart.CartHeader.CartHeaderId;
            cart.CartDetails.FirstOrDefault().Product = null;
            _db.CartDetails.Add(cart.CartDetails.FirstOrDefault());
            await _db.SaveChangesAsync();
        }
        else
        {
            // if header != null check if details has same product
            var cartDetailsFromDb = await _db.CartDetails
                .AsNoTracking()
                .FirstOrDefaultAsync(
                cartDetails => cartDetails.ProductId == cart.CartDetails.FirstOrDefault().ProductId &&
                               cartDetails.CartHeaderId == cartHeaderFromDb.CartHeaderId);

            if (cartDetailsFromDb == null)
            {
                //create details
                cart.CartDetails.FirstOrDefault().CartHeaderId = cartHeaderFromDb.CartHeaderId;
                cart.CartDetails.FirstOrDefault().Product = null;
                _db.CartDetails.Add(cart.CartDetails.FirstOrDefault());
                await _db.SaveChangesAsync();
            }
            else
            {
                //update count or cart details
                cart.CartDetails.FirstOrDefault().Product = null;
                cart.CartDetails.FirstOrDefault().Count += cartDetailsFromDb.Count;
                _db.CartDetails.Update(cart.CartDetails.FirstOrDefault());
                await _db.SaveChangesAsync();
            }
        }

        return _mapper.Map<CartDTO>(cart);
    }

    public async Task<bool> RemoveFromCart(int cartDetailsId)
    {
        try
        {
            var cartDetails = await _db.CartDetails
                .FirstOrDefaultAsync(details => details.CartDetailsId == cartDetailsId);

            int totalCountOfCartItems = _db.CartDetails
                .Where(header => header.CartHeaderId == cartDetails.CartHeaderId)
                .Count();
            _db.CartDetails.Remove(cartDetails);

            if (totalCountOfCartItems == 1)
            {
                var cartHeaderToRemove = await _db.CartHeaders
                    .FirstOrDefaultAsync(header => header.CartHeaderId == cartDetails.CartHeaderId);
                _db.CartHeaders.Remove(cartHeaderToRemove);
            }

            await _db.SaveChangesAsync();

            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return false;
        }
    }

    public async Task<bool> ClearCart(string userId)
    {
        var cartHeaderFromDb = await _db.CartHeaders
            .FirstOrDefaultAsync(cartHeader => cartHeader.UserId == userId);

        if (cartHeaderFromDb != null)
        {
            _db.CartDetails.RemoveRange(_db.CartDetails
                .Where(details => details.CartHeaderId == cartHeaderFromDb.CartHeaderId));
            _db.CartHeaders.Remove(cartHeaderFromDb);
            await _db.SaveChangesAsync();

            return true;
        }

        return false;
    }
}