using Graduation_Project.Models;

namespace Graduation_Project.Repository.CategoryRepository
{
    public interface ICategoryRepository
    {
        void Delete(int id);
        List<Category> GetAllCategories();
        Category GetCategoryById(int id);
        Category GetCategoryByName(string name);

        void Insert(Category category);
        int Save();
        void Update(Category categories);
    }
}