using Graduation_Project.Models;

namespace Graduation_Project.DTO
{
    public class ShowAllProductsInThisCategoryDto
    {

        public string Title { get; set; }
        //=====================
        public string Description { get; set; }
        //=====================
        public decimal Price { get; set; }
        //=====================
        public string ImgUrl { get; set; }
        //=====================
        public double Rating { get; set; }
        //=====================
        public int Stock { get; set; }
        //=====================
        public string CategoryName { get; set; }
        //=====================
        public string BrandName { get; set; }

        //public List<Product>? productList { get; set; }

    }
}
