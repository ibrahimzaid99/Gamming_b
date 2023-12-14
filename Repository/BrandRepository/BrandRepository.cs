using Graduation_Project.Models;
using Microsoft.EntityFrameworkCore;

namespace Graduation_Project.Repository.BrandRepository
{
    public class BrandRepository : IBrandRepository
    {

        GraduationProject_Context context;

        public BrandRepository(GraduationProject_Context _context)
        {
            this.context = _context;
        }
        //============================================
        public List<Brand> GetAllBrandes()
        {
            return context.Brands.Include(s => s.Products).ToList();
        }
        //============================================
        public Brand GetBrandById(int id)
        {

            return context.Brands.FirstOrDefault(brand => brand.Id == id);
        }
        //============================================
        public void Update(Brand Brandes)
        {
            context.Update(Brandes);
        }
        //============================================
        public void Delete(int id)
        {
            Brand OldBrand = GetBrandById(id);
            context.Remove(OldBrand);
        }
        //============================================
        public void Insert(Brand brand)
        {
            context.Brands.Add(brand);
        }
        //============================================
        public int Save()
        {
            return context.SaveChanges();
        }
    }
}
