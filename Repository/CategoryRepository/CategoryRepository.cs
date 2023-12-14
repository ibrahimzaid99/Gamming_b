using Graduation_Project.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace Graduation_Project.Repository.CategoryRepository
{
    public class CategoryRepository : ICategoryRepository
    {
        GraduationProject_Context context;

        public CategoryRepository(GraduationProject_Context _context)
        {
            this.context = _context;
        }
        //============================================
        public List<Category> GetAllCategories()
        {
            return context.Categories.Include(s=>s.Products).ToList();
        }
        //============================================
        public Category GetCategoryById(int id)
        {

            return context.Categories.FirstOrDefault(category =>category.Id == id);
        }
        //============================================
        public Category GetCategoryByName(string name)
        {

            return context.Categories.FirstOrDefault(category => category.Name.ToLower() == name.ToLower());
        }
        //============================================
        public void Update(Category categories)
        {
            context.Update(categories);
        }
        //============================================
        public void Delete(int id)
        {
            Category OldCategory = GetCategoryById(id);
            context.Remove(OldCategory);
        }
        //============================================
        public void Insert(Category category)
        {
            context.Categories.Add(category);
        }
        //============================================
        public int Save()
        {
            return context.SaveChanges();
        }

    }
}
