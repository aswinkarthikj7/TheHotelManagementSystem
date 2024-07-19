using Core.Interfaces;
using Core.Models;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace TheHotelManagementSystem.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpPost]
        public async Task<ActionResult<Category?>> CreateCategory(Category category)
        {
            return await _categoryService.CreateAsync(category);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Category>>> GetAll()
            => Ok(await _categoryService.GetAllAsync());

        [HttpPut]
        public async Task<ActionResult<ReplaceOneResult?>> UpdateCategory(Category category)
        {
            return await _categoryService.UpdateAsync(category.Id, category);
        }

        [HttpDelete]
        public async Task<ActionResult<DeleteResult?>> DeleteCategory(string id)
        {
            return await _categoryService.DeleteAsync(id);
        }
    }
}
