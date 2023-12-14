using static Graduation_Project.Models.Order;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Graduation_Project.Models
{
    public class OrderItem
    {
        public int Id { get; set; }
        //=====================
        public int Amount { get; set; }
        //=====================
        public decimal Price { get; set; }
        //=====================
        [ForeignKey("Product")]
        public int ProductId { get; set; }
        public virtual Product? Product { get; set; }
        //=====================
        [ForeignKey("Order")]
        public int OrderId { get; set; }
        [JsonIgnore]
        public virtual Order? Order { get; set; }
        //=====================
    }
}
