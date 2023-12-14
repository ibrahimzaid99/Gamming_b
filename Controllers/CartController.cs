using Graduation_Project.Models;
using Graduation_Project.Models.Authontication;
using Graduation_Project.Repository.CartRepository;
using Graduation_Project.Repository.ProductRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Graduation_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CartController : ControllerBase
    {
        private readonly ICartItemRepository cartItemRepository;
        private readonly IProductRepository productRepository;
        private readonly UserManager<ApplicationIdentityUser> UserManager;
        public CartController(ICartItemRepository cartItemRepository, IProductRepository productRepository, UserManager<ApplicationIdentityUser> UserManager)
        {
            this.cartItemRepository = cartItemRepository;
            this.productRepository = productRepository;
            this.UserManager = UserManager;
        }
        //==============================================

        [HttpGet]
        public async Task<IActionResult> GetUserCart()
        {
            // Get the currently authenticated user
            ApplicationIdentityUser user = await UserManager.GetUserAsync(User);

            if (user == null)
            {
                return Unauthorized("User not authenticated");
            }

            // Get cart items for the current user
            List<CartItem> userCartItems = cartItemRepository.GetUserCarts(user.Id);

            return Ok(userCartItems);
        }

        //==============================================

        [HttpPost]
        public async Task<IActionResult> AddToCart([FromBody] int id)
        {
            // Get the currently authenticated user
            ApplicationIdentityUser user = await UserManager.GetUserAsync(User);

            // Get cart items for the current user
            List<CartItem> userCartItems = cartItemRepository.GetUserCarts(user.Id);

            // Check if the user already has the same product in their cart
            CartItem existingCartItem = userCartItems.FirstOrDefault(cart => cart.ProductId == id);

            if (existingCartItem != null)
            {
                // If the product already exists in the user's cart, update the quantity
                existingCartItem.Amount += 1;
                cartItemRepository.Update(existingCartItem);
                return Ok(existingCartItem);
            }
            else
            {
                // If the product doesn't exist in the user's cart, create a new cart item
                CartItem newCartItem = new CartItem()
                {
                    ProductId = id,
                    UserId = user.Id,
                    Amount = 1
                };

                // Save the new cart item
                cartItemRepository.AddToCart(newCartItem);
                return Ok(newCartItem);
            }
        }
        //==============================================

        [HttpPut]
        [Route("{id:int}")]
        public IActionResult EditCartItem([FromRoute]int id, [FromBody] CartItem updatedCartItem)
        {
            // Retrieve the existing cart item from the database
            CartItem existingCartItem = cartItemRepository.GetCartById(id);

            // Check if the cart item exists
            if (existingCartItem == null)
            {
                return NotFound("Cart item not found");
            }

            // Update the cart item properties
            existingCartItem.Amount = updatedCartItem.Amount;

            // Save the changes to the database
            cartItemRepository.Update(existingCartItem);

            return Ok(existingCartItem);
        }
        //==============================================

        [HttpPut]
        [Route("increase/{id:int}")]
        public IActionResult IncreaseAmount([FromRoute] int id)
        {
            // Retrieve the existing cart item from the database
            CartItem existingCartItem = cartItemRepository.GetCartById(id);

            // Check if the cart item exists
            if (existingCartItem == null)
            {
                return NotFound("Cart item not found");
            }

            // Update the cart item properties
            existingCartItem.Amount += 1;

            // Save the changes to the database
            cartItemRepository.Update(existingCartItem);

            return Ok(existingCartItem);
        }
        [HttpPut]
        [Route("decrease/{id:int}")]
        public IActionResult DecreaseAmount([FromRoute] int id)
        {
            // Retrieve the existing cart item from the database
            CartItem existingCartItem = cartItemRepository.GetCartById(id);

            // Check if the cart item exists
            if (existingCartItem == null)
            {
                return NotFound("Cart item not found");
            }

            if(existingCartItem.Amount > 1)
            {
                // Update the cart item properties
                existingCartItem.Amount -= 1;
                // Save the changes to the database
                cartItemRepository.Update(existingCartItem);

                return Ok(existingCartItem);

            }
            else
            {
                cartItemRepository.RemoveFromCart(id);

                return Ok("Cart item deleted successfully");
            }
       
        }
        [HttpDelete]
        [Route("{id:int}")]
        public IActionResult DeleteCartItem(int id)
        {
            // Retrieve the cart item from the database
            CartItem cartItem = cartItemRepository.GetCartById(id);

            // Check if the cart item exists
            if (cartItem == null)
            {
                return NotFound("Cart item not found");
            }

            // Delete the cart item from the database
            cartItemRepository.RemoveFromCart(id);

            return Ok("Cart item deleted successfully");
        }
    }
}