using Graduation_Project.Models;
using System.ComponentModel.DataAnnotations;

namespace Graduation_Project.DTO
{
    public class CategoryDto
    {
        public int Id { get; set; }
        //=====================
        public string Name { get; set; }
        //=====================
        //[Required]
        //[RegularExpression(@"^\w+\.(png|jpg)$", ErrorMessage = "Image Must Be (png or Jpg)")]
        public string ImgUrl { get; set; }
        //=====================
        //public List<Product>? productList { get; set; }

    }
}
