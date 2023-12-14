using Graduation_Project.Models.Authontication;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using static Graduation_Project.Models.Order;

namespace Graduation_Project.Models
{
    public class GraduationProject_Context : IdentityDbContext<ApplicationIdentityUser>
    {
        public GraduationProject_Context()
        {

        }
        public GraduationProject_Context(DbContextOptions Options) : base(Options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog =Graduation_Project;Integrated Security=True;Encrypt=False");
        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<WishListItem>? wishListItems { get; set; }
    }
}
