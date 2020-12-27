using Onebrb.Data;
using Onebrb.Services.Mapping;
using Onebrb.Services.Models.Category;
using System.Collections.Generic;
using System.Linq;
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

            if (categoriesDb == null)
            {
                return Enumerable.Empty<CategoryServiceModel>().ToList();
            }

            var categoriesReturned = ObjectMapper.Mapper.Map<ICollection<CategoryServiceModel>>(categoriesDb);

            return categoriesReturned;
        }
    }
}
