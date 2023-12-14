using Graduation_Project.Models;
using Graduation_Project.Repository.CategoryRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http.HttpResults;
using Graduation_Project.DTO;
using Graduation_Project.Repository.ProductRepository;

namespace Graduation_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        ICategoryRepository categoryRepository;

        public CategoriesController(ICategoryRepository CategRepo)
        {
            categoryRepository = CategRepo;

        }
        //=============================================

        [HttpGet]
        public IActionResult GetAllCategories()
        {
            List<Category> categoriesModel = categoryRepository.GetAllCategories();
            List<CategoryDto> categoryDtos = new List<CategoryDto>();
            foreach (Category category in categoriesModel)
            {
                var NewDto = new CategoryDto()
                {
                    Id = category.Id,
                    Name = category.Name,
                    ImgUrl = category.ImgUrl,
                };
                categoryDtos.Add(NewDto);
            }
            return Ok(categoryDtos);
        }
        //=============================================
        [HttpGet]
        [Route("{id:int}", Name = "GetCategoryById")]
        public IActionResult GetCategoryById(int id)
        {
            Category category = categoryRepository.GetCategoryById(id);
            CategoryDto CatDto = new CategoryDto()
            {
                Id = id,
                Name = category.Name,
                ImgUrl = category.ImgUrl
            };
            return Ok(CatDto);
        }
        //=============================================
        [HttpGet]
        [Route("{name:alpha}", Name = "GetCategoryByName")]
        public IActionResult GetCategoryByName(string name)
        {
            Category category = categoryRepository.GetCategoryByName(name);
            CategoryDto CatDto = new CategoryDto()
            {
                
                Name = category.Name,
                ImgUrl = category.ImgUrl
            };
            return Ok(CatDto);
        }
        //=============================================
        [HttpPost]
        public IActionResult AddNewCategory(CategoryDto categoryDto)
        {
            if (ModelState.IsValid == true)
            {
                Category category = new Category
                {
                    Name = categoryDto.Name,
                    ImgUrl = categoryDto.ImgUrl
                };

                categoryRepository.Insert(category);
            
                categoryRepository.Save();
                    categoryDto.Id= category.Id;
                string url = Url.Link("GetCategoryById", new { id = category.Id });
                return Created(url, categoryDto);
            }
            else
            {
                return BadRequest("No Category Added");
            }
        }
        //=============================================
        [HttpPut]
        [Route("{id:int}")]
        public IActionResult EditeCategory([FromRoute] int id, [FromBody] CategoryDto categoryDto)
        {

            if (ModelState.IsValid == true)
            {
                Category OldCategory = categoryRepository.GetCategoryById(id);

                if (OldCategory != null)
                {
                    OldCategory.Id = id;
                    OldCategory.Name = categoryDto.Name;
                    OldCategory.ImgUrl = categoryDto.ImgUrl;
                    categoryRepository.Save();
                    return StatusCode(204, OldCategory);
                }
                else
                {
                    return NotFound("Category not found");
                }
            }
            else
            {
                return BadRequest(ModelState);
            }
        }
        //=============================================

        [HttpDelete("{id:int}")]
        public IActionResult RemoveCategory(int id)
        {
            Category OldCategory = categoryRepository.GetCategoryById(id);
            if (OldCategory != null)
            {
                try
                {
                    categoryRepository.Delete(id);
                    categoryRepository.Save();
                    return StatusCode(204, "Record Removed Successfuly");
                }
                catch
                {
                    return BadRequest(ModelState);
                }
            }
            return BadRequest("This ID Not Found");
        }

    }
}
