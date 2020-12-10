using Onebrb.Services.Models.Category;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Onebrb.Services.Categories
{
    public interface ICategoryService
    {
        Task<ICollection<CategoryServiceModel>> GetAllCategoriesAsync();
    }
}