using Graduation_Project.DTO;
using Graduation_Project.Models;
using Graduation_Project.Repository.BrandRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Graduation_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandsController : ControllerBase
    {
        IBrandRepository brandRepository;
        public BrandsController( IBrandRepository BrandRepo)
        {
            brandRepository = BrandRepo;
        }

        [HttpGet]
        public IActionResult GetAllBrandes()
        {
            List<Brand> BrandesModel = brandRepository.GetAllBrandes();
            List<BrandDto> BrandDtos = new List<BrandDto>();
            foreach (Brand brand in BrandesModel)
            {
                var NewDto = new BrandDto()
                {
                    Id = brand.Id,
                    Name = brand.Name,
                    ImageUrl= brand.ImageUrl,
                };
                BrandDtos.Add(NewDto);
            }
            return Ok(BrandDtos);
        }
        //=============================================
        [HttpGet]
        [Route("{id:int}", Name = "GetBrandById")]
        public IActionResult GetBrandById(int id)
        {
            Brand brand = brandRepository.GetBrandById(id);
            BrandDto brandDto = new BrandDto()
            {
                Id = brand.Id,
                Name = brand.Name,
                ImageUrl = brand.ImageUrl
            };
            return Ok(brandDto);
        }
        //=============================================  
        [HttpPost]
        public IActionResult AddNewBrand(BrandDto brandDto)
        {
            if (ModelState.IsValid == true)
            {
                Brand brand = new Brand
                {
                    Name = brandDto.Name,
                    ImageUrl = brandDto.ImageUrl
                };
                brandRepository.Insert(brand);
                brandRepository.Save();
                string url = Url.Link("GetBrandById", new { id = brand.Id });
                return Created(url,brandDto);
            }
            else
            {
                return BadRequest("No Brand Added");
            }
        }
        //=============================================
        [HttpPut]
        [Route("{id:int}")]
        public IActionResult EditeBrand([FromRoute] int id, [FromBody] BrandDto brandDto)
        {

            if (ModelState.IsValid == true)
            {
                Brand OldBrand = brandRepository.GetBrandById(id);

                if (OldBrand != null)
                {
                    OldBrand.Name = brandDto.Name;
                    OldBrand.ImageUrl = brandDto.ImageUrl;
                    brandRepository.Save();
                    return StatusCode(204, OldBrand);
                }
                else
                {
                    return NotFound("Brand not found");
                }
            }
            else
            {
                return BadRequest(ModelState);
            }
        }
        //=============================================

        [HttpDelete("{id:int}")]
        public IActionResult RemoveBrand(int id)
        {
            Brand OldBrand = brandRepository.GetBrandById(id);
            if (OldBrand != null)
            {
                try
                {
                    brandRepository.Delete(id);
                    brandRepository.Save();
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
