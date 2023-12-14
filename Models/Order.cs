using Graduation_Project.Models.Authontication;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Graduation_Project.Models
{
    public class Order
    {

        public int Id { get; set; }
        //=====================
        public decimal TotalPrice { get; set; }
        //=====================
        public string Government { get; set; }
        //=====================
        public string Street { get; set; }
        //=====================
        public string City { get; set; }
        //=====================
        public string PostalCode { get; set; }
        //=====================
        public string BulidingNumber { get; set; }
        //=====================
        public int PhoneNumber { get; set; }
        //=====================
        public DateTime? OrderDate { get; set; }
        //=====================
        public bool IsCancelled { get; set; }
        //=====================
        public bool IsCompleted { get; set; }
        //=====================
        [ForeignKey("ApplicationIdentityUser")]
        public string? UserId { get; set; }
        [JsonIgnore]
        public virtual ApplicationIdentityUser? User { get; set; }
        //=======================
        public virtual List<OrderItem>? OrderItems { get; set; }
        //=====================

    }
}
