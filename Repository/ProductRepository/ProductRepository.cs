using Graduation_Project.Models;
using Microsoft.EntityFrameworkCore;

namespace Graduation_Project.Repository.ProductRepository
{
    public class ProductRepository : IProductRepository
    {
        GraduationProject_Context context;
        public ProductRepository(GraduationProject_Context _context)
        {
            this.context = _context;
        }
        public List<Product> GetAllProducts()
        {
            return context.Products.Include(p => p.Brand).Include(p => p.Category).ToList();
        }
        //============================================
        public Product GetProductById(int id)
        {

            return context.Products.Include(p => p.Brand).Include(p => p.Category).FirstOrDefault(p => p.Id == id);
        }
        //============================================
        public void Update(Product product)
        {
            context.Update(product);
        }
        //============================================
        public void Delete(int id)
        {
            Product OldProduct = GetProductById(id);
            context.Remove(OldProduct);
        }
        //============================================
        public void Insert(Product product)
        {
            context.Products.Add(product);
        }
        //============================================
        public int Save()
        {
            return context.SaveChanges();
        }
        public Product GetProductByIdTwo(int id)
        {

            return context.Products.FirstOrDefault(p => p.Id == id);
        }
    }
}
