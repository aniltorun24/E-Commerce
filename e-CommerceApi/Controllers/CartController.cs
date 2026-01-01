using e_CommerceApi.Dto;
using e_CommerceApi.Models.Context;
using e_CommerceApi.Models.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata.Ecma335;

namespace e_CommerceApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly Context _context;

        public CartController(Context context)
        {
            _context = context;
        }


        [HttpGet]
        public async Task<ActionResult<CartDto>> GetCart()
        {
            return CartToDto(await GetOrCreate());

            
        }

        [HttpPost]
        public async  Task<IActionResult> AddItemToCart(int productId, int quantity)
        {
            var cart = await GetOrCreate();

            var product = await _context.Products.FirstOrDefaultAsync(i => i.Id == productId);

            if (product == null)
            {
                return NotFound("the product is not in database");
            }
            cart.AddItem(product, quantity);

            var result = await _context.SaveChangesAsync() > 0 ;

            if (result) 
                return CreatedAtAction(nameof(GetCart), CartToDto(cart));

            return BadRequest(new ProblemDetails { Title = "The product can not be added to cart"});


        }

        [HttpDelete]
        public async Task<IActionResult> RemoveItemFromCart(int productId, int quantity)
        {
            var cart = await GetOrCreate();
            cart.DeleteItem(productId, quantity);
            var result = await _context.SaveChangesAsync() > 0;
            if (result)
                return CreatedAtAction(nameof(GetCart), CartToDto(cart));
            return BadRequest(new ProblemDetails { Title = "There is a problem while trying remove the item from the cart" });
        }
        private async Task<Cart> GetOrCreate()
        {
            var cart = await _context.Carts
                .Include(i => i.CartItems)
                .ThenInclude(i => i.Product)
                .Where(i => i.CustomerId == Request.Cookies["customerId"])
                .FirstOrDefaultAsync();

            if (cart == null) 
            {
                var customerId = Guid.NewGuid().ToString();

                var cookieOptions = new CookieOptions
                {
                    Expires = DateTime.Now.AddMonths(1),
                    IsEssential = true
                };

                Response.Cookies.Append("customerId", customerId, cookieOptions);

                cart = new Cart { CustomerId = customerId };

                _context.Carts.Add(cart);
                await _context.SaveChangesAsync();
            }
            return cart;
        } 

        private CartDto CartToDto(Cart cart)
        {
            return new CartDto
            {
                CartId = cart.CartId,
                CustomerId = cart.CustomerId,
                CartItems = cart.CartItems.Select(i => new CartItemDto
                {
                    ProductId = i.ProductId,
                    Name = i.Product.Name,
                    Price = i.Product.Price,
                    ImageUrl = i.Product.ImageUrl,
                    Quantity = i.Quantity
                }).ToList()
            };
        }


    }
}
