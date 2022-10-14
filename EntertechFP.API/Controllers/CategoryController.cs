using EntertechFP.API.Attributes;
using EntertechFP.API.Responses;
using EntertechFP.BL.Abstract;
using EntertechFP.BL.Concrete;
using EntertechFP.DAL.Abstract;
using EntertechFP.DAL.Concrete;
using EntertechFP.DAL.Concrete.Contexts;
using EntertechFP.EL.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EntertechFP.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiKey]
    public class CategoryController : ControllerBase
    {
        private ICategoryService categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            this.categoryService = categoryService;
        }

        [HttpGet]
        public BaseResponse<List<Category>> GetAll(int include = 0)
            => include == 0
            ? new BaseResponse<List<Category>>(categoryService.GetAll())
            : new BaseResponse<List<Category>>(categoryService.GetAll(null, c => c.Events));

        [HttpGet("{id}")]
        public BaseResponse<Category> Get(int id, int include = 0)
            => include == 0
            ? new BaseResponse<Category>(categoryService.Get(c => c.CategoryId == id))
            : new BaseResponse<Category>(categoryService.Get(c => c.CategoryId == id, c => c.Events));

        [HttpPost]
        public BaseResponse<Category> Add([FromBody] Category category)
        {
            var data = categoryService.Get(c => c.CategoryName.Equals(category.CategoryName));
            if (data is null)
            {
                categoryService.Add(category);
                return new BaseResponse<Category>(category);
            }
            return new BaseResponse<Category>("Kategori ismi mevcut");
        }
        [HttpPatch("{id}")]
        public BaseResponse<Category> Update(int id,[FromBody] Category category)
        {
            var updatedCategory = Get(id).Data;
            if (categoryService.Get(c => c.CategoryName.Equals(category.CategoryName)) is not null)
                return new BaseResponse<Category>("Kategori ismi mevcut");
            updatedCategory.CategoryName = category.CategoryName;
            categoryService.Update(updatedCategory);
            return new BaseResponse<Category>(updatedCategory);
        }

        [HttpDelete("{id}")]
        public BaseResponse<Category> Delete(int id)
        {
            var category = Get(id).Data;
            if (category is null)
                return new BaseResponse<Category>("Bu id'ye ait bir kategori bulunamadı.");
            categoryService.Delete(category);
            return new BaseResponse<Category>(true);
        }
    }
}
