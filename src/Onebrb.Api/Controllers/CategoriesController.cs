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

        [HttpGet]
        public async Task<BaseApiResponse<ICollection<CategoryServiceModel>>> GetAllCategories()
        {
            var categories = await _categoryService.GetAllCategoriesAsync();

            if (categories == null)
            {
                new BaseApiResponse<ICollection<CategoryServiceModel>>
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    Message = "Couldn't fetch categories.",
                    Body = Enumerable.Empty<CategoryServiceModel>().ToList()
                };
            }

            return new BaseApiResponse<ICollection<CategoryServiceModel>>
            {
                StatusCode = StatusCodes.Status200OK,
                Message = "List of categories",
                Body = categories
            };
        }
    }
}
