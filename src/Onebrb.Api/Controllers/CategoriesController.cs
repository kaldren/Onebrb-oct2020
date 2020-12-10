using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Onebrb.Api.Helpers;
using Onebrb.Core.Models;
using Onebrb.Services.Categories;
using Onebrb.Services.Models.Category;

namespace Onebrb.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public async Task<IActionResult> GetAllCategoriesAsync()
        {
            var categories = await _categoryService.GetAllCategoriesAsync();

            if (categories == null)
            {
                return BadRequest();
            }

            return Ok(new BaseApiResponse<IEnumerable<CategoryServiceModel>>
            {
                StatusCode = StatusCodes.Status200OK,
                Message = "List of categories",
                Response = categories
            });
        }
    }
}
