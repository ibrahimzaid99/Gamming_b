using Graduation_Project.Models;

namespace Graduation_Project.Repository.BrandRepository
{
    public interface IBrandRepository
    {
        void Delete(int id);
        List<Brand> GetAllBrandes();
        Brand GetBrandById(int id);
        void Insert(Brand brand);
        int Save();
        void Update(Brand Brandes);
    }
}