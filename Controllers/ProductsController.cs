using Graduation_Project.DTO;
using Graduation_Project.Models;
using Graduation_Project.Repository.BrandRepository;
using Graduation_Project.Repository.CategoryRepository;
using Graduation_Project.Repository.ProductRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Graduation_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {

        private readonly IProductRepository productRepository;
        private readonly ICategoryRepository categoryRepository;
        private readonly IBrandRepository brandRepository;

        public ProductsController(IProductRepository productRepo, ICategoryRepository categoryRepo, IBrandRepository brandRepo)
        {
            productRepository = productRepo;
            categoryRepository = categoryRepo;
            brandRepository = brandRepo;
        }


        [HttpGet]
        public IActionResult GetAllProducts()
        {
            List<Product> products = productRepository.GetAllProducts();
            List<ProductDto> productDtos = new List<ProductDto>();
            foreach (Product product in products)
            {
                ProductDto NewDto = new ProductDto()
                {
                    Title = product.Title,
                    Price = product.Price,
                    Description = product.Description,
                    Id = product.Id,
                    ImgUrl = product.ImgUrl,
                    BrandName = product.Brand.Name,
                    CategoryName = product.Category.Name,
                    Rating = product.Rating,
                    SKU = product.SKU,
                    Stock = product.Stock,
                };
                productDtos.Add(NewDto);
            }
            return Ok(productDtos);
        }
        //=============================================
        [HttpGet]
        [Route("{id:int}", Name = "GetProductById")]
        public IActionResult GetProductById(int id)
        {
            Product product = productRepository.GetProductById(id);
            ProductDto productDto = new ProductDto()
            {
                Title = product.Title,
                Price = product.Price,
                Description = product.Description,
                Id = product.Id,
                ImgUrl = product.ImgUrl,
                BrandName = product.Brand.Name.ToString(),
                CategoryName = product.Category.Name.ToString(),
                Rating = product.Rating,
                SKU = product.SKU,
                Stock = product.Stock,
            };
            return Ok(productDto);
        }
        //=============================================
        [HttpPost]
        public IActionResult AddNewProduct(Product newproduct)
        {

            if (ModelState.IsValid == true)
            {
                productRepository.Insert(newproduct);
                productRepository.Save();
                string url = Url.Link("GetProductById", new { id = newproduct.Id });
                return Created(url, newproduct);
            }
            else
            {
                return BadRequest("No Product Added");
            }
        }
        //=============================================
        [HttpPut]
        [Route("{id:int}")]
        public IActionResult EditeProduct([FromRoute] int id, [FromBody] Product product)
        {

            if (ModelState.IsValid == true)
            {
                Product Oldproduct = productRepository.GetProductById(id);

                if (Oldproduct != null)
                {

                    Oldproduct.Title = product.Title;
                    Oldproduct.ImgUrl = product.ImgUrl;
                    Oldproduct.Rating = product.Rating;
                    Oldproduct.SKU = product.SKU;
                    Oldproduct.BrandId = product.BrandId;
                    Oldproduct.Stock = product.Stock;
                    Oldproduct.Price = product.Price;
                    Oldproduct.Category_Id = product.Category_Id;
                    Oldproduct.Description = product.Description;
                    productRepository.Save();
                    return StatusCode(204, Oldproduct);
                }
                else
                {
                    return NotFound("Product not found");
                }
            }
            else
            {
                return BadRequest(ModelState);
            }
        }
        //=============================================
        [HttpDelete("{id:int}")]
        public IActionResult RemoveProduct(int id)
        {
            Product Oldproduct = productRepository.GetProductById(id);
            if (Oldproduct != null)
            {
                try
                {
                    productRepository.Delete(id);
                    productRepository.Save();
                    return StatusCode(204, "Record Removed Successfuly");
                }
                catch
                {
                    return BadRequest(ModelState);
                }
            }
            return BadRequest("This ID Not Found");
        }
        //=============================================
        [HttpGet]
        [Route("category/{id}")]
        public IActionResult Category(int id) 
        {
            List<Product> Products = productRepository.GetAllProducts().Where(product => product.Category.Id == id).ToList();
            List<ProductDto> productDtos = new List<ProductDto>();
            foreach (Product product in Products)
            {
                ProductDto NewDto = new ProductDto()
                {
                    Title = product.Title,
                    Price = product.Price,
                    Description = product.Description,
                    Id = product.Id,
                    ImgUrl = product.ImgUrl,
                    BrandName = product.Brand.Name,
                    CategoryName = product.Category.Name,
                    Rating = product.Rating,
                    SKU = product.SKU,
                    Stock = product.Stock,
                };
                productDtos.Add(NewDto);
            }
            return Ok(productDtos);
        }
        //=============================================
        [HttpGet]
        [Route("category/{name:alpha}")]
        public IActionResult Category(string name)
        {
            List<Product> Products = productRepository.GetAllProducts().Where(product => product.Category.Name.ToLower() == name.ToLower()).ToList();
            List<ProductDto> productDtos = new List<ProductDto>();
            foreach (Product product in Products)
            {
                ProductDto NewDto = new ProductDto()
                {
                    Title = product.Title,
                    Price = product.Price,
                    Description = product.Description,
                    Id = product.Id,
                    ImgUrl = product.ImgUrl,
                    BrandName = product.Brand.Name,
                    CategoryName = product.Category.Name,
                    Rating = product.Rating,
                    SKU = product.SKU,
                    Stock = product.Stock,
                };
                productDtos.Add(NewDto);
            }
            return Ok(productDtos);
        }
        [HttpGet]
        [Route("brand/{id:int}")]
        public IActionResult Brand(int id)
        {
            List<Product> Products = productRepository.GetAllProducts().Where(product => product.Brand.Id == id).ToList();
            List<ProductDto> productDtos = new List<ProductDto>();
            foreach (Product product in Products)
            {
                ProductDto NewDto = new ProductDto()
                {
                    Title = product.Title,
                    Price = product.Price,
                    Description = product.Description,
                    Id = product.Id,
                    ImgUrl = product.ImgUrl,
                    BrandName = product.Brand.Name,
                    CategoryName = product.Category.Name,
                    Rating = product.Rating,
                    SKU = product.SKU,
                    Stock = product.Stock,
                };
                productDtos.Add(NewDto);
            }
            return Ok(productDtos);
        }
        [HttpGet]
        [Route("brand/{name:alpha}")]
        public IActionResult Brand(string name)
        {
            List<Product> Products = productRepository.GetAllProducts().Where(product => product.Brand.Name.ToLower() == name.ToLower()).ToList();
            List<ProductDto> productDtos = new List<ProductDto>();
            foreach (Product product in Products)
            {
                ProductDto NewDto = new ProductDto()
                {
                    Title = product.Title,
                    Price = product.Price,
                    Description = product.Description,
                    Id = product.Id,
                    ImgUrl = product.ImgUrl,
                    BrandName = product.Brand.Name,
                    CategoryName = product.Category.Name,
                    Rating = product.Rating,
                    SKU = product.SKU,
                    Stock = product.Stock,
                };
                productDtos.Add(NewDto);
            }
            return Ok(productDtos);
        }
    }

}

