using AutoMapper;
using Onebrb.Core.Models;
using Onebrb.Data;
using Onebrb.Services.Mapping;
using Onebrb.Services.Models.Category;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Onebrb.Services.Categories
{
    public class CategoryService : ICategoryService
    {
        private readonly IOnebrbContext _dbContext;

        public CategoryService(IOnebrbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ICollection<CategoryServiceModel>> GetAllCategoriesAsync()
        {
            var categoriesDb = await _dbContext.GetAllCategories();

            var categoriesReturned = ObjectMapper.Mapper.Map<ICollection<CategoryServiceModel>>(categoriesDb);

            return categoriesReturned;
        }
    }
}
