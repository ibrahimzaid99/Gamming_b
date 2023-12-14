using Graduation_Project.Models.Authontication;
using System.ComponentModel.DataAnnotations.Schema;

namespace Graduation_Project.Models
{
    public class WishListItem
    {
        public int Id { get; set; }
        //=====================
        [ForeignKey("Product")]
        public int? ProductId { get; set; }
        public virtual Product? Product { get; set; }
        //=====================
        [ForeignKey("ApplictionIdentityUser")]
        public string? UserId { get; set; }
        public virtual ApplicationIdentityUser? User { get; set; }
        //=====================
    }
}
