using Onebrb.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Onebrb.Services.Categories
{
    public interface ICategoryService
    {
        Task<ICollection<Category>> GetAllCategoriesAsync();
    }
}