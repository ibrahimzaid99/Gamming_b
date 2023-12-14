using Graduation_Project.Models;

namespace Graduation_Project.Repository.WishListRepository
{
    public interface IWishListRepository
    {
        void AddToWishList(WishListItem WishList);
        List<WishListItem> GetUserWishList(string userId);
        WishListItem GetWishListById(int WishListId);
        void RemoveFromWishList(int WishListId);
        int Save();
        void Update(WishListItem WishList);
    }
}