using System.ComponentModel.DataAnnotations;

namespace Graduation_Project.DTO
{
    public class LoginUserDto
    {
        [Required]
        public string UserName { get; set; }
        //=====================
        [Required]
        public string Password { get; set; }
        //=====================

    }
}
