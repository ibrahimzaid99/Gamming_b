using Microsoft.AspNetCore.Identity;

namespace Graduation_Project.Models.Authontication
{
    public class ApplicationIdentityUser : IdentityUser
    {
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
        public virtual List<CartItem>? CartItems { get; set; }
    }
}
