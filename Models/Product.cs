using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Graduation_Project.Models
{
    public class Product
    {
        public int Id { get; set; }
        //=====================
        [Required(ErrorMessage = "You Didn't Entry A Title To This Product")]
        public string Title { get; set; }
        //=====================
        [Required(ErrorMessage = "You Didn't Entry A Price To This Product")]
        public decimal Price { get; set; }
        //=====================
        [Required(ErrorMessage = "You Didn't Entry A Description To This Product")]
        public string Description { get; set; }
        //=====================
        [Required(ErrorMessage = "You Didn't Entry An Image To This Product")]
        [RegularExpression(@"^\w+\.(png|jpg)$", ErrorMessage = "Image Must Be (png or Jpg)")]
        public string ImgUrl { get; set; }
        //=====================
        [Required(ErrorMessage = "You Didn't Entry An Rating To This Product")]
        [Range(minimum: 0, maximum: 5, ErrorMessage = "Range Must Be Between(0=>5)")]
        public double Rating { get; set; }
        //=====================
        public int Stock { get; set; }
        //=====================
        public int SKU { get; set; }
        //=====================
        [ForeignKey("Category")]
        public int? Category_Id { get; set; }
        [JsonIgnore]
        public virtual Category? Category { get; set; }
        //=====================
        [ForeignKey("Brand")]
        public int? BrandId { get; set; }
        [JsonIgnore]
        public virtual Brand? Brand { get; set; }
        // --------------------------------
        //public List<CartItem>? CartItems { get; set; }
        // --------------------------------





    }
}
