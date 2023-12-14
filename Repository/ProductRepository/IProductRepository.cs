using Graduation_Project.Models;

namespace Graduation_Project.Repository.ProductRepository
{
    public interface IProductRepository
    {
        void Delete(int id);
        List<Product> GetAllProducts();
        Product GetProductById(int id);
        Product GetProductByIdTwo(int id);

        void Insert(Product product);
        int Save();
        void Update(Product product);
    }
}