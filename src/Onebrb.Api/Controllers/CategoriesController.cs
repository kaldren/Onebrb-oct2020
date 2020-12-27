using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Onebrb.Api.Constants;
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

            if (categories.Count < 1)
            {
                new BaseApiResponse<ICollection<CategoryServiceModel>>
                {
                    StatusCode = StatusCodes.Status404NotFound,
                    Message = ResponseMessages.NotFound,
                };
            }

            return new BaseApiResponse<ICollection<CategoryServiceModel>>
            {
                StatusCode = StatusCodes.Status200OK,
                Message = ResponseMessages.SuccessfulOperation,
                Body = categories
            };
        }
    }
}
