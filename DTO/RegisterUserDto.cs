using System.ComponentModel.DataAnnotations;

namespace Graduation_Project.DTO
{
    public class RegisterUserDto
    {
        [Required]
        public string UserName { get; set; }
        //=====================
        [Required]
        public string Password { get; set; }
        //=====================
        //[Required]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }
        //=====================
        public string Email { get; set; }
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

    }
}
