using Graduation_Project.DTO;
using Graduation_Project.Models.Authontication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Graduation_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<ApplicationIdentityUser> userManager;
        private readonly IConfiguration configuration;

        public AccountController(UserManager<ApplicationIdentityUser> userManager,IConfiguration configuration)
        {
            this.userManager = userManager;
            this.configuration = configuration;
        }
        //creat Account User(Registration) "Post"
        [HttpPost("register")] //api/account/register
        public async Task<IActionResult> Registration(RegisterUserDto userDto)
        {

            if (ModelState.IsValid == true)
            {
                ApplicationIdentityUser user = new ApplicationIdentityUser();
                user.UserName = userDto.UserName;
                user.Email = userDto.Email;
                user.Government = userDto.Government;
                user.BulidingNumber = userDto.BulidingNumber;
                user.Street = userDto.Street;
                user.City = userDto.City;
                user.PhoneNumber = userDto.PhoneNumber;
                user.PostalCode = userDto.PostalCode;
                IdentityResult result = await userManager.CreateAsync(user, userDto.Password);
                if (result.Succeeded)
                {
                    return Ok("Account Add Successfully");
                }
                else
                {
                    var errors = new List<string>();
                    foreach (var error in result.Errors)
                    {
                        errors.Add($"{error.Code}: {error.Description}");
                    }

                    return BadRequest(errors);
                }
            }
            return BadRequest(ModelState);
        }

        //Check Account Valid (Login)"Post"

        [HttpPost("login")] //api/account/login
        public async Task<IActionResult> Login(LoginUserDto userDto)
        {

            if (ModelState.IsValid == true)
            {
                //check and creat token
                ApplicationIdentityUser user = await userManager.FindByNameAsync(userDto.UserName);

                if (user != null)
                {
                    bool found = await userManager.CheckPasswordAsync(user, userDto.Password);
                    if (found == true)
                    {

                        // claims Token
                        var claims = new List<Claim>();
                        claims.Add(new Claim(ClaimTypes.Name, user.UserName));
                        claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id));
                        claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));


                        //get role
                        var roles = await userManager.GetRolesAsync(user);
                        foreach (var itemRole in roles)
                        {
                            claims.Add(new Claim(ClaimTypes.Role, itemRole));
                        }
                        SecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secret"]));

                        SigningCredentials signInCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

                        //creat token
                        JwtSecurityToken MyToken = new JwtSecurityToken(
                            issuer: "http://localhost:5118/", //url web api
                            audience: "http://localhost:4200/", //url consumer angular
                            claims: claims,
                            expires: DateTime.Now.AddHours(3),
                            signingCredentials:signInCredentials
                            );
                        return Ok(new
                        {
                            token = new JwtSecurityTokenHandler().WriteToken(MyToken),
                            user = user,
                            roles= roles,
                            expiration = MyToken.ValidTo
                        });
                    }
                }
            }
            return Unauthorized();

        }
    }
}
