using System.ComponentModel.DataAnnotations;

namespace Graduation_Project.Models
{
    public class Brand
    {
        public int Id { get; set; }
        //=====================
        public string Name { get; set; }
        //=====================
        public string ImageUrl { get; set; }
        //=====================
        public virtual List<Product>? Products { get; set; }
        //=====================


    }
}
