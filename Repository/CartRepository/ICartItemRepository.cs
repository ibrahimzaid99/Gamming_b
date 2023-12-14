using Graduation_Project.Models;

namespace Graduation_Project.Repository.CartRepository
{
    public interface ICartItemRepository
    {
        void AddToCart(CartItem cartItem);
        List<CartItem> GetAllCarts();
         
        CartItem GetCartById(int cartId);
        List<CartItem> GetUserCarts(string userId);
        void RemoveFromCart(int cartItemId);
        int Save();
        void Update(CartItem cart);
    }
}