using System.ComponentModel.DataAnnotations;

namespace Graduation_Project.Models
{
    public class Category
    {
        public int Id { get; set; }
        //=====================
        [Required(ErrorMessage = "Name Of Categoy Is Empty, You Must Enter A Name")]
        [MinLength(2)]
        [MaxLength(20)]
        public string Name { get; set; }
        //=====================
        [Required]
        [RegularExpression(@"^\w+\.(png|jpg)$", ErrorMessage = "Image Must Be (png or Jpg)")]
        public string ImgUrl { get; set; }
        //=====================
        public virtual List<Product>? Products { get; set; }
        //=====================

    }
}
