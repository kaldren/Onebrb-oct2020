using Onebrb.Core.Models;
using Onebrb.Data;
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

        public Task<ICollection<Category>> GetAllCategories()
        {
            return _dbContext.GetAllCategories();
        }
    }
}
