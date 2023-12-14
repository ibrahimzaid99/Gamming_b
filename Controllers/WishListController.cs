using Graduation_Project.Models.Authontication;
using Graduation_Project.Models;
using Graduation_Project.Repository.CartRepository;
using Graduation_Project.Repository.ProductRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Graduation_Project.Repository.WishListRepository;

namespace Graduation_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WishListController : ControllerBase
    {

        private readonly ICartItemRepository cartItemRepository;
        private readonly IProductRepository productRepository;
        private readonly UserManager<ApplicationIdentityUser> UserManager;
        private readonly IWishListRepository wishListRepository;

        public WishListController(ICartItemRepository cartItemRepository, IProductRepository productRepository, UserManager<ApplicationIdentityUser> UserManager,IWishListRepository wishListRepository)
        {
            this.cartItemRepository = cartItemRepository;
            this.productRepository = productRepository;
            this.UserManager = UserManager;
            this.wishListRepository = wishListRepository;
        }
        //==============================================

        [HttpGet]
        public async Task<IActionResult> GetUserWishList()
        {
            // Get the currently authenticated user
            ApplicationIdentityUser user = await UserManager.GetUserAsync(User);

            if (user == null)
            {
                return Unauthorized("User not authenticated");
            }

            // Get wishlist items for the current user
            List<WishListItem> userWishListItems = wishListRepository.GetUserWishList(user.Id);

            return Ok(userWishListItems);
        }

        //==============================================

        [HttpPost]
        public async Task<IActionResult> AddToWishList([FromBody] int id)
        {
            // Get the currently authenticated user
            ApplicationIdentityUser user = await UserManager.GetUserAsync(User);

            // Get wishlist items for the current user
            List<WishListItem> userWishListItems = wishListRepository.GetUserWishList(user.Id);

            // Check if the user already has the same product in their wishlist
            WishListItem existingWishListItem = userWishListItems.FirstOrDefault(wishlist => wishlist.ProductId == id);

            if (existingWishListItem != null)
            {
                return Ok(existingWishListItem);
            }
            else
            {
                // If the product doesn't exist in the user's wishlist, create a new wishlist item
                WishListItem newWishListItem = new WishListItem()
                {
                    ProductId = id,
                    UserId = user.Id,
                };

                // Save the new wishlist item
                wishListRepository.AddToWishList(newWishListItem);
                return Ok(newWishListItem);
            }
        }
        //==============================================

        [HttpDelete]
        [Route("{id:int}")]
        public IActionResult DeleteWishListItem(int id)
        {
            // Retrieve the wishlist item from the database
            WishListItem wishListItem = wishListRepository.GetWishListById(id);

            // Check if the wishlist item exists
            if (wishListItem == null)
            {
                return NotFound("WishList item not found");
            }

            // Delete the wishlist item from the database
            wishListRepository.RemoveFromWishList(id);

            return Ok("WishList item deleted successfully");
        }
    }
}
