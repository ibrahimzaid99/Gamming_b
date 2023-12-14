using Graduation_Project.Models;
using Microsoft.EntityFrameworkCore;

namespace Graduation_Project.Repository.CartRepository
{
    public class CartItemRepository : ICartItemRepository
    {
        private readonly GraduationProject_Context context;

        public CartItemRepository(GraduationProject_Context _context)
        {
            context = _context;
        }

        public List<CartItem> GetAllCarts()
        {

            return context.CartItems.Include(c => c.Product).ToList();
        }
        public List<CartItem> GetUserCarts(string userId)
        {
            return context.CartItems
                          .Include(c => c.Product)
                          .Where(cartItem => cartItem.UserId == userId)
                          .ToList();
        }
        //------------------------------------
        public CartItem GetCartById(int cartId)
        {

            return context.CartItems.FirstOrDefault(cart => cart.Id == cartId);
        }
        //------------------------------------
        public void Update(CartItem cart)
        {
            context.Update(cart);
            context.SaveChanges();
        }
        //------------------------------------
        public void RemoveFromCart(int cartItemId)
        {
            var cart = context.CartItems.Find(cartItemId);
            if (cart != null)
            {
                context.CartItems.Remove(cart);
                context.SaveChanges();
            }
        }
        //------------------------------------
        public void AddToCart(CartItem cartItem)
        {
            context.CartItems.Add(cartItem);
            context.SaveChanges();
        }
        //------------------------------------
        public int Save()
        {
            return context.SaveChanges();
        }
    }
}

