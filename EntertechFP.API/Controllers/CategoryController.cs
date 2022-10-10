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
        [HttpPatch("{id}/{categoryname}")]
        public BaseResponse<Category> Update(int id, string categoryName)
        {
            var category = categoryService.Get(c => c.CategoryId == id);
            if (categoryService.Get(c => c.CategoryName.Equals(categoryName)) is not null)
                return new BaseResponse<Category>("Kategori ismi mevcut");
            category.CategoryName = categoryName;
            categoryService.Update(category);
            return new BaseResponse<Category>(category);
        }

        [HttpDelete("{id}")]
        public BaseResponse<Category> Delete(int id)
        {
            var category = categoryService.Get(c => c.CategoryId == id);
            if (category is null)
                return new BaseResponse<Category>("Bu id'ye ait bir kategori bulunamadı.");
            categoryService.Delete(category);
            return new BaseResponse<Category>(true);
        }
    }
}
