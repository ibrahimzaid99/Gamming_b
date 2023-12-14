using Graduation_Project.Models;
using Microsoft.EntityFrameworkCore;

namespace Graduation_Project.Repository.WishListRepository
{
    public class WishListRepository : IWishListRepository
    {
        private readonly GraduationProject_Context context;

        public WishListRepository(GraduationProject_Context _context)
        {
            context = _context;
        }


        public List<WishListItem> GetUserWishList(string userId)
        {
            return context.wishListItems
                          .Include(c => c.Product)
                          .Where(cartItem => cartItem.UserId == userId)
                          .ToList();
        }
        //------------------------------------
        public WishListItem GetWishListById(int WishListId)
        {

            return context.wishListItems.FirstOrDefault(cart => cart.Id == WishListId);
        }
        //------------------------------------
        public void Update(WishListItem WishList)
        {
            context.Update(WishList);
            context.SaveChanges();
        }
        //------------------------------------
        public void RemoveFromWishList(int WishListId)
        {
            var WishList = context.wishListItems.Find(WishListId);
            if (WishList != null)
            {
                context.wishListItems.Remove(WishList);
                context.SaveChanges();
            }
           

        }
        //------------------------------------
        public void AddToWishList(WishListItem WishList)
        {
            context.wishListItems.Add(WishList);
            context.SaveChanges();
        }
        //------------------------------------
        public int Save()
        {
            return context.SaveChanges();
        }
    }
}
