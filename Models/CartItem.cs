using Graduation_Project.Models.Authontication;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Graduation_Project.Models
{
    public class CartItem
    {
        public int Id { get; set; }
        //=====================

        [ForeignKey("Product")]
        public int ProductId { get; set; }
        //[JsonIgnore]
        public virtual Product? Product { get; set; }
        //=====================
        [ForeignKey("ApplictionIdentityUser")]
        public string? UserId { get; set; }
        [JsonIgnore]

        public virtual ApplicationIdentityUser? User { get; set; }
        //=====================
        public int Amount { get; set; }
        //=====================


    }
}
